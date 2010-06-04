using NUnit.Framework;
using Restfulie.Server.Tests.Fixtures;
using Restfulie.Server.Unmarshalling.Deserializers.Xml;

namespace Restfulie.Server.Tests.Unmarshalling.Deserializers
{
    [TestFixture]
    public class XmlDeserializerTests
    {
        [Test]
        public void ShouldDeserializeResourceInXml()
        {
            const string xml = "<SomeResource><Name>Some name</Name><Amount>100.00</Amount></SomeResource>";
            var deserializer = new XmlDeserializer();
            var resource = deserializer.DeserializeResource(xml, typeof(SomeResource)) as SomeResource;

            Assert.AreEqual("Some name", resource.Name);
            Assert.AreEqual(100M, resource.Amount);
        }

        [Test]
        public void ShouldDeserializeListOfResourcesInXml()
        {
            const string xml = 
                "<SomeResources>"+
                "<SomeResource><Name>John Doe</Name><Amount>200.00</Amount></SomeResource>" +
                "<SomeResource><Name>Sally Doe</Name><Amount>400.00</Amount></SomeResource>" +
                "</SomeResources>";
            var deserializer = new XmlDeserializer();
            var resources = (SomeResource[])deserializer.DeserializeList(xml, typeof(SomeResource));

            Assert.AreEqual("John Doe", resources[0].Name);
            Assert.AreEqual(200M, resources[0].Amount);
            Assert.AreEqual("Sally Doe", resources[1].Name);
            Assert.AreEqual(400M, resources[1].Amount);
        }
    }
}
