using System;
using NUnit.Framework;
using Restfulie.Server.Marshalling.Serializers;
using Restfulie.Server.Marshalling.Serializers.Json;
using Restfulie.Server.Tests.Fixtures;

namespace Restfulie.Server.Tests.Marshalling.Serializers.Json
{
    [TestFixture]
    public class JsonSerializerTests
    {
        private IResourceSerializer serializer;

        [SetUp]
        public void SetUp()
        {
            serializer = new JsonSerializer();   
        }

        [Test]
        public void ShouldSerializeAsResource()
        {
            var resource = new SomeResource {Amount = 123.45, Name = "John Doe", UpdatedAt = new DateTime(2012,7,29)};
            
            var json = serializer.Serialize(resource);

            Assert.That(json.Contains("\"Name\":\"John Doe\""));
            Assert.That(json.Contains("\"Amount\":123.45"));
            Assert.That(json.Contains("\"UpdatedAt\":\"2012-07-29T00:00:00\""));
        }
        
        [Test]
        public void ShouldSerializeAListOfResources()
        {
            var resources = new []
                                          {
                                              new SomeResource {Amount = 123.45, Name = "John Doe"},
                                              new SomeResource {Amount = 67.89, Name = "Sally Doe"}
                                          };

            var json = serializer.Serialize(resources);

            Assert.That(json.Contains("\"Name\":\"John Doe\""));
            Assert.That(json.Contains("\"Amount\":123.45"));
            Assert.That(json.Contains("\"Name\":\"Sally Doe\""));
            Assert.That(json.Contains("\"Amount\":67.89"));

        }
    }
}