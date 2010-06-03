using NUnit.Framework;
using Restfulie.Server.Marshalling;
using Restfulie.Server.Marshalling.Serializers.XmlAndHypermedia;
using Restfulie.Server.Negotiation;
using Restfulie.Server.Unmarshalling;

namespace Restfulie.Server.Tests.Negotiation
{
    [TestFixture]
    public class ContentNegotiationTests
    {
        [Test]
        public void ShouldReturnSerializerForApplicationXml()
        {
            var serializer = new DefaultContentNegotiation().ForRequest("application/xml");

            Assert.AreEqual("application/xml", serializer.FriendlyName);
            Assert.IsTrue(serializer.Marshaller is RestfulieMarshaller);
            Assert.IsTrue(serializer.Unmarshaller is RestfulieUnmarshaller);
        }

        [Test]
        public void ShouldThrowAnExceptionIfRequestedMediaTypeIsInvalid()
        {
        	Assert.Throws<RequestedMediaTypeNotSupportedException>(() =>
                new DefaultContentNegotiation().ForRequest("some-crazy-media-type"));
        }

        [Test]
        public void ShouldThrowAnExceptionIfResponseMediaTypeIsInvalid()
        {
            Assert.Throws<ResponseMediaTypeNotSupportedException>(() =>
                new DefaultContentNegotiation().ForResponse("some-crazy-media-type"));
        }
    }
}
