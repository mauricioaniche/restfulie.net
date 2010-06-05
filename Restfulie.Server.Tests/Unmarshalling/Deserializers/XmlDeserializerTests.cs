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
            var resource = deserializer.Deserialize(xml, typeof(SomeResource)) as SomeResource;

            Assert.AreEqual("Some name", resource.Name);
            Assert.AreEqual(100M, resource.Amount);
        }

        [Test]
        public void ShouldDeserializeListOfResourcesInXml()
        {
            const string xml = 
                "<ArrayOfSomeResource>"+
                    "<SomeResource><Name>John Doe</Name><Amount>123.45</Amount></SomeResource>"+
                    "<SomeResource><Name>Sally Doe</Name><Amount>67.89</Amount></SomeResource>"+
                "</ArrayOfSomeResource>";
            var deserializer = new XmlDeserializer();
            var resources = (SomeResource[])deserializer.Deserialize(xml, typeof(SomeResource[]));

            Assert.AreEqual("John Doe", resources[0].Name);
            Assert.AreEqual(123.45, resources[0].Amount);
            Assert.AreEqual("Sally Doe", resources[1].Name);
            Assert.AreEqual(67.89, resources[1].Amount);
        }
    }
}
