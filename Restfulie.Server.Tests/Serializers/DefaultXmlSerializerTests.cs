using System;
using NUnit.Framework;
using Restfulie.Server.Serializers;

namespace Restfulie.Server.Tests.Serializers
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
            
            var xml = serializer.Serialize(resource);

            Assert.That(xml.Contains("<Name>John Doe</Name>"));
            Assert.That(xml.Contains("<Amount>123.45</Amount>"));
        }

    }
}
