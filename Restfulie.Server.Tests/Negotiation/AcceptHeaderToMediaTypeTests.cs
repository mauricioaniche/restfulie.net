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
        private Mock<IMediaTypeList> mediaTypeList;

        [SetUp]
        public void SetUp()
        {
            mediaType1 = new Mock<IMediaType>();
            mediaType1.SetupGet(m => m.Synonyms).Returns(new[] { "application/xml", "text/xml" });

            mediaType2 = new Mock<IMediaType>();
            mediaType2.SetupGet(m => m.Synonyms).Returns(new[] {"application/atom+xml"});

            mediaTypeList = new Mock<IMediaTypeList>();
            mediaTypeList.Setup(m => m.Find("application/xml")).Returns(mediaType1.Object);
            mediaTypeList.Setup(m => m.Find("text/xml")).Returns(mediaType1.Object);
            mediaTypeList.Setup(m => m.Find("application/atom+xml")).Returns(mediaType2.Object);
            mediaTypeList.SetupGet(m => m.MediaTypes).Returns(new[] {mediaType1.Object, mediaType2.Object});

            acceptHeader = new AcceptHeaderToMediaType(mediaTypeList.Object);
            
        }

        [Test]
        public void ShouldReturnMediaTypeForASimpleExpression()
        {
            var mediaType = acceptHeader.GetMediaType("application/xml");

            Assert.AreEqual(mediaType1.Object, mediaType);
        }

        [Test]
        public void ShouldReturnMediaTypeForASynonym()
        {
            var mediaType = acceptHeader.GetMediaType("text/xml");

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
