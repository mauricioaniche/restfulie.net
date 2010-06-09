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

            config.Register<XmlAndHypermedia, XmlSerializer, XmlDeserializer>();

            Assert.IsTrue(config.GetSerializer<XmlAndHypermedia>() is XmlSerializer);
            Assert.IsTrue(config.GetDeserializer<XmlAndHypermedia>() is XmlDeserializer);
        }

        [Test]
        public void ShouldReturnNullIfNotRegistered()
        {
            var config = new RestfulieConfiguration();

            Assert.IsNull(config.GetSerializer<XmlAndHypermedia>());
            Assert.IsNull(config.GetDeserializer<XmlAndHypermedia>());
        }
    }
}
