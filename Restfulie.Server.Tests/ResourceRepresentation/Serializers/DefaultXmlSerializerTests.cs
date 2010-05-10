using System;
using System.Collections.Generic;
using NUnit.Framework;
using Restfulie.Server.ResourceRepresentation.Serializers;
using Restfulie.Server.Tests.Fixtures;

namespace Restfulie.Server.Tests.ResourceRepresentation.Serializers
{
    [TestFixture]
    public class DefaultXmlSerializerTests
    {
        private DefaultXmlSerializer serializer;

        [SetUp]
        public void SetUp()
        {
            serializer = new DefaultXmlSerializer();   
        }

        [Test]
        public void ShouldSerializeAllDataInResource()
        {
            var resource = new SomeResource {Amount = 123.45, Name = "John Doe"};
            
            var xml = serializer.Serialize(resource, new List<Transition>());

            Assert.That(xml.Contains("<Name>John Doe</Name>"));
            Assert.That(xml.Contains("<Amount>123.45</Amount>"));
        }
        
        [Test]
        public void ShouldSerializeAllTransitions()
        {
            var resource = new SomeResource { Amount = 123.45, Name = "John Doe" };

            var xml = serializer.Serialize(resource,
                                           new List<Transition> { new Transition("pay", "controller", "action", "url") });

            Assert.That(xml.Contains("rel=\"pay\""));  
        }
    }
}