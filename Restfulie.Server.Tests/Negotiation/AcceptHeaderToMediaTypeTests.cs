using Moq;
using NUnit.Framework;
using Restfulie.Server.MediaTypes;
using Restfulie.Server.Negotiation;

namespace Restfulie.Server.Tests.Negotiation
{
    [TestFixture]
    public class AcceptHeaderToMediaTypeTests
    {
        private Mock<IMediaType> mediaType2;
        private Mock<IMediaType> mediaType1;
        private AcceptHeaderToMediaType acceptHeader;

        [SetUp]
        public void SetUp()
        {
            mediaType1 = new Mock<IMediaType>();
            mediaType1.SetupGet(m => m.Synonyms).Returns(new[] { "application/xml", "text/xml" });

            mediaType2 = new Mock<IMediaType>();
            mediaType2.SetupGet(m => m.Synonyms).Returns(new[] {"application/atom+xml"});

            acceptHeader = new AcceptHeaderToMediaType(new[] {mediaType1.Object, mediaType2.Object});
            
        }

        [Test]
        public void ShouldReturnMediaTypeForASimpleExpression()
        {
            var mediaType = acceptHeader.GetMediaType("application/xml");

            Assert.AreEqual(mediaType1.Object, mediaType);
        }

        [Test]
        public void ShouldReturnMostQualifiedMediaType()
        {
            var mediaType = acceptHeader.GetMediaType("application/atom+xml; q=1.0, application/xml; q=0.8");

            Assert.AreEqual(mediaType2.Object, mediaType);
        }

        [Test]
        public void ShouldReturnMostQualifiedMediaTypeWhenQualifierIsNotPresent()
        {
            var mediaType = acceptHeader.GetMediaType("application/atom+xml; q=0.8, application/xml");

            Assert.AreEqual(mediaType2.Object, mediaType);
        }

        [Test]
        public void ShouldThrowAnExceptionIfMediaTypeIsNotAccepted()
        {
            Assert.Throws<RequestedMediaTypeNotSupportedException>(() => acceptHeader.GetMediaType("some-crazy-media-type"));
        }
    }
}
