using NUnit.Framework;
using Restfulie.Server.Tests.Fixtures;
using Restfulie.Server.Unmarshalling.Deserializers.Json;
using Restfulie.Server.Unmarshalling.Deserializers;

namespace Restfulie.Server.Tests.Unmarshalling.Deserializers
{
    [TestFixture]
    public class JsonDeserializerTests
    {
        private IResourceDeserializer deserializer;

        [SetUp]
        public void SetUp()
        {
            deserializer = new JsonDeserializer();
        }

        [Test]
        public void ShouldDeserializeResourceInJson()
        {
            var json = "{ \"Name\":\"John Doe\", \"Amount\":123.45 }";
            var resource = deserializer.Deserialize(json, typeof(SomeResource)) as SomeResource;

            Assert.AreEqual("John Doe", resource.Name);
            Assert.AreEqual(123.45, resource.Amount);
        }

        [Test]
        public void ShouldDeserializeListOfResourcesInJson()
        {
            var json =
                   "[" +
                        "{\"Name\":\"John Doe\", \"Amount\":123.45 }," +
                        "{\"Name\":\"Sally Doe\", \"Amount\":67.89 }" +
                    "]";
            var resources = (SomeResource[])deserializer.Deserialize(json, typeof(SomeResource[]));

            Assert.AreEqual("John Doe", resources[0].Name);
            Assert.AreEqual(123.45, resources[0].Amount);
            Assert.AreEqual("Sally Doe", resources[1].Name);
            Assert.AreEqual(67.89, resources[1].Amount);
        }
    }
}
