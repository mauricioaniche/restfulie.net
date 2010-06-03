using Moq;
using NUnit.Framework;
using Restfulie.Server.MediaTypes;
using Restfulie.Server.Negotiation;

namespace Restfulie.Server.Tests.Negotiation
{
    [TestFixture]
    public class ContentTypeToMediaTypeTests
    {
        private Mock<IMediaType> atom;
        private Mock<IMediaType> xml;
        private ContentTypeToMediaType contentType;
        private Mock<IMediaTypeList> mediaTypeList;

        [SetUp]
        public void SetUp()
        {
            xml = new Mock<IMediaType>();
            xml.SetupGet(m => m.Synonyms).Returns(new[] { "application/xml", "text/xml" });

            atom = new Mock<IMediaType>();
            atom.SetupGet(m => m.Synonyms).Returns(new[] { "application/atom+xml" });

            mediaTypeList = new Mock<IMediaTypeList>();
            mediaTypeList.Setup(m => m.Find("application/xml")).Returns(xml.Object);
            mediaTypeList.Setup(m => m.Find("text/xml")).Returns(xml.Object);
            mediaTypeList.Setup(m => m.Find("application/atom+xml")).Returns(atom.Object);
            mediaTypeList.SetupGet(m => m.Default).Returns(xml.Object);
            mediaTypeList.SetupGet(m => m.MediaTypes).Returns(new[] { xml.Object, atom.Object });

            contentType = new ContentTypeToMediaType(mediaTypeList.Object);

        }

        [Test]
        public void ShouldReturnMediaType()
        {
            var mediaType = contentType.GetMediaType("application/xml");

            Assert.AreEqual(xml.Object, mediaType);
        }

        [Test]
        public void ShouldThrowExceptionWhenContentTypeIsNotAccepted()
        {
            Assert.Throws<ResponseMediaTypeNotSupportedException>(() => contentType.GetMediaType("some-crazy-media-type"));
        }
    }
}
