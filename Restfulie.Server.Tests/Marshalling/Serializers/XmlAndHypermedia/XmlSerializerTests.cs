using System;
using System.Collections.Generic;
using NUnit.Framework;
using Restfulie.Server.Marshalling.Serializers;
using Restfulie.Server.Marshalling.Serializers.XmlAndHypermedia;
using Restfulie.Server.Tests.Fixtures;

namespace Restfulie.Server.Tests.Marshalling.Serializers.XmlAndHypermedia
{
    [TestFixture]
    public class XmlSerializerTests
    {
        private IResourceSerializer serializer;

        [SetUp]
        public void SetUp()
        {
            serializer = new XmlSerializer();   
        }

        [Test]
        public void ShouldSerializeAllDataInResource()
        {
            var resource = new SomeResource {Amount = 123.45, Name = "John Doe"};
            
            var xml = serializer.Serialize(resource);
            Assert.That(xml.Contains("<Name>John Doe</Name>"));
            Assert.That(xml.Contains("<Amount>123.45</Amount>"));
        }
        
        [Test]
        public void ShouldSerializeAListOfResources()
        {
            var resources = new []
                                          {
                                              new SomeResource {Amount = 123.45, Name = "John Doe"},
                                              new SomeResource {Amount = 67.89, Name = "Sally Doe"}
                                          };

            var xml = serializer.Serialize(resources);

            Assert.That(xml.Contains("John Doe"));
            Assert.That(xml.Contains("Sally Doe"));
        }
    }
}