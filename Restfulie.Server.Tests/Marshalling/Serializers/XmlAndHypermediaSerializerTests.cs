using System;
using System.Collections.Generic;
using NUnit.Framework;
using Restfulie.Server.Marshalling.Serializers;
using Restfulie.Server.Marshalling.Serializers.XmlAndHypermedia;
using Restfulie.Server.Tests.Fixtures;

namespace Restfulie.Server.Tests.Marshalling.Serializers
{
    [TestFixture]
    public class XmlAndHypermediaSerializerTests
    {
        private IResourceSerializer serializer;

        [SetUp]
        public void SetUp()
        {
            serializer = new XmlAndHypermediaSerializer();   
        }

        [Test]
        public void ShouldSerializeAllDataInResource()
        {
            var resource = new SomeResource {Amount = 123.45, Name = "John Doe"};
            
            var xml = serializer.Serialize(resource, new List<Relation>());

            Assert.That(xml.Contains("<Name>John Doe</Name>"));
            Assert.That(xml.Contains("<Amount>123.45</Amount>"));
        }
        
        [Test]
        public void ShouldSerializeAllTransitions()
        {
            var resource = new SomeResource { Amount = 123.45, Name = "John Doe" };

            var xml = serializer.Serialize(resource, SomeRelations());

            Assert.That(xml.Contains("rel=\"pay\""));
            Assert.That(xml.Contains("<atom:link"));
            Assert.That(xml.Contains("http://some/url")); 
        }

        [Test]
        public void ShouldSerializeAListOfResources()
        {
            var resourcesXrelations = new Dictionary<IBehaveAsResource, IList<Relation>>
                                          {
                                              {new SomeResource {Amount = 123.45, Name = "John Doe"}, SomeRelations()},
                                              {new SomeResource {Amount = 67.89, Name = "Sally Doe"}, SomeRelations()}
                                          };

            var xml = serializer.Serialize(resourcesXrelations, "SomeResources");

            Assert.That(xml.Contains("John Doe"));
            Assert.That(xml.Contains("Sally Doe"));
            Assert.That(xml.Contains("<SomeResources>"));
        }

        [Test]
        public void ShouldReturnFormat()
        {
            Assert.AreEqual("application/xml", serializer.Format);
        }

        private IList<Relation> SomeRelations()
        {
            return new List<Relation> {new Relation("pay", "controller", "action", "http://some/url")};
        }
    }
}