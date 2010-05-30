using NUnit.Framework;
using Restfulie.Server.Tests.Fixtures;
using Restfulie.Server.Unmarshalling.Deserializers.Xml;

namespace Restfulie.Server.Tests.Unmarshalling.Deserializers
{
    [TestFixture]
    public class XmlDeserializerTests
    {
        [Test]
        public void ShouldDeserializeXml()
        {
            const string xml = "<SomeResource><Name>Some name</Name><Amount>100.00</Amount></SomeResource>";
            var deserializer = new XmlDeserializer();
            var resource = deserializer.Deserialize(xml, typeof(SomeResource)) as SomeResource;

            Assert.AreEqual("Some name", resource.Name);
            Assert.AreEqual(100M, resource.Amount);
        }
    }
}
