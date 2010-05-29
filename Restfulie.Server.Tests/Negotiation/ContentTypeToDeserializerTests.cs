using NUnit.Framework;
using Restfulie.Server.Exceptions;
using Restfulie.Server.Negotitation;
using Restfulie.Server.Unmarshalling.Deserializers;

namespace Restfulie.Server.Tests.Negotiation
{
    [TestFixture]
    public class ContentTypeToDeserializerTests
    {
        [Test]
        public void ShouldReturnDeserializerForAGivenContentType()
        {
            var deserializer = new ContentTypeToDeserializer().For("application/xml");

            Assert.IsTrue(deserializer is XmlDeserializer);
        }

        [Test]
        public void ShouldReturnDefaultIfMediaTypeIsNotProvided()
        {
            var serializer = new ContentTypeToDeserializer().For(null);

            Assert.IsTrue(serializer is XmlDeserializer);
        }

        [Test]
        [ExpectedException(typeof(ContentTypeNotSupportedException))]
        public void ShouldThrowAnExceptionIfMediaTypeIsInvalid()
        {
            new ContentTypeToDeserializer().For("some-crazy-media-type");
        }

    }
}
