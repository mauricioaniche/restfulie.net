using System.Globalization;
using Moq;
using NUnit.Framework;
using Restfulie.Server.MediaTypes;
using Restfulie.Server.Negotiation;

namespace Restfulie.Server.Tests.Negotiation
{
    [TestFixture]
    public class AcceptHeaderToMediaTypeTests
    {
        private Mock<IMediaType> atom;
        private Mock<IMediaType> xml;
        private AcceptHeaderToMediaType acceptHeader;
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

            acceptHeader = new AcceptHeaderToMediaType(mediaTypeList.Object);

        }

        [Test]
        public void ShouldReturnMediaTypeForASimpleExpression()
        {
            var mediaType = acceptHeader.GetMediaType("application/xml");

            Assert.AreEqual(xml.Object, mediaType);
        }

        [Test]
        public void ShouldReturnMediaTypeForASynonym()
        {
            var mediaType = acceptHeader.GetMediaType("text/xml");

            Assert.AreEqual(xml.Object, mediaType);
        }

        [Test]
        public void ShouldReturnMostQualifiedMediaType()
        {
            var mediaType = acceptHeader.GetMediaType("application/atom+xml; q=1.0, application/xml; q=0.8");
            Assert.AreEqual(atom.Object, mediaType);

            mediaType = acceptHeader.GetMediaType("application/atom+xml; q=0.8, application/xml; q=1.0");
            Assert.AreEqual(xml.Object, mediaType);
        }

        [Test]
        public void ShouldReturnMostQualifiedMediaTypeWhenItsNotTheFirstMediaTypeSpecified()
        {
            var mediaType = acceptHeader.GetMediaType("application/atom+xml; q=0.8, application/xml; q=1.0");

            Assert.AreEqual(xml.Object, mediaType);
        }

        [Test]
        public void ShouldReturnMostQualifiedMediaTypeWhenQualifierIsNotPresent()
        {
            var mediaType = acceptHeader.GetMediaType("application/atom+xml; q=0.8, application/xml");

            Assert.AreEqual(xml.Object, mediaType);
        }

        [Test]
        public void ShouldReturnMostQualifiedMediaTypeWhenMoreThenOneQualifierParameterIsPresent()
        {
            var mediaType =
                acceptHeader.GetMediaType(
                    "application/xml,application/vnd.wap.xhtml+xml,application/xhtml+xml;profile='http://www.wapforum.org/xhtml',text/html;q=0.9,text/plain;q=0.8,image/png,*/*;q=0.5");

            Assert.AreEqual(xml.Object, mediaType);
        }

        [TestCase("pt-BR")]
        public void ShouldSetQualifiedOneDotZeroWhenQualifiedIsNotPresentCultureIndependent(string culture)
        {
            System.Threading.Thread.CurrentThread.CurrentCulture = new CultureInfo(culture);

            var mediaType = acceptHeader.GetMediaType("application/atom+xml; q=0.8, application/xml");
            //the mediaType should xml because qualified default is .1.0
            Assert.AreEqual(xml.Object, mediaType);
        }

        [Test]
        public void ShouldThrowAnExceptionIfMediaTypeIsNotAccepted()
        {
            Assert.Throws<AcceptHeaderNotSupportedException>(() => acceptHeader.GetMediaType("some-crazy-media-type"));
        }

        [Test]
        public void ShouldReturnDefaultMediaType()
        {
            var mediaType = acceptHeader.GetMediaType("*/*");

            Assert.AreEqual(xml.Object, mediaType);
        }

        [Test]
        public void ShouldReturnDefaultMediaTypeEvenIfItHasQualifier()
        {
            var mediaType = acceptHeader.GetMediaType("*/*; q=0.5");

            Assert.AreEqual(xml.Object, mediaType);
        }

        [Test]
        public void ShouldReturnMostQualifiedMediaTypeWhenDefaultHasQualifier()
        {
            var mediaType = acceptHeader.GetMediaType("*/*; q=0.5, application/atom+xml");

            Assert.AreEqual(atom.Object, mediaType);
        }

        [Test]
        public void ShouldReturnDefaultMediaTypeIsNotIsProvided()
        {
            var mediaType = acceptHeader.GetMediaType(null);
            Assert.AreEqual(xml.Object, mediaType);

            mediaType = acceptHeader.GetMediaType("");
            Assert.AreEqual(xml.Object, mediaType);
        }
    }
}
