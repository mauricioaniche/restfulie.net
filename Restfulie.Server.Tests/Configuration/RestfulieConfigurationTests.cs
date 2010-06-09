using NUnit.Framework;
using Restfulie.Server.Configuration;
using Restfulie.Server.Marshalling.Serializers.XmlAndHypermedia;
using Restfulie.Server.MediaTypes;
using Restfulie.Server.Unmarshalling.Deserializers.Xml;

namespace Restfulie.Server.Tests.Configuration
{
    [TestFixture]
    public class RestfulieConfigurationTests
    {
        [Test]
        public void ShouldRegisterSerializerAndDeserializerForAMediaType()
        {
            var config = new RestfulieConfiguration();

            config.Register<XmlAndHypermedia, XmlAndHypermediaSerializer, XmlDeserializer>();

            Assert.IsTrue(config.GetSerializer<XmlAndHypermedia>() is XmlAndHypermediaSerializer);
            Assert.IsTrue(config.GetDeserializer<XmlAndHypermedia>() is XmlDeserializer);
        }
    }
}
