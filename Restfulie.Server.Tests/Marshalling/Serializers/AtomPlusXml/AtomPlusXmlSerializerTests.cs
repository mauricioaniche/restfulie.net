using System;
using System.Collections.Generic;
using NUnit.Framework;
using Restfulie.Server.Marshalling.Serializers;
using Restfulie.Server.Marshalling.Serializers.AtomPlusXml;
using Restfulie.Server.Tests.Fixtures;

namespace Restfulie.Server.Tests.Marshalling.Serializers.AtomPlusXml
{
    [TestFixture]
    public class AtomPlusXmlSerializerTests
    {
        private IResourceSerializer serializer;

        [SetUp]
        public void SetUp()
        {
            serializer = new AtomPlusXmlSerializer();
        }

        [Test]
        public void ShouldSerializeAResource()
        {
            var date = new DateTime(2010, 10, 10);
            var resource = new SomeResource { Name = "John Doe", Amount = 123.45, Id = 123, UpdatedAt = date};
            var atom = serializer.Serialize(resource);

            var expectedResult =
                "<entry xmlns=\"http://www.w3.org/2005/Atom\">\r\n  <title>Restfulie.Server.Tests.Fixtures.SomeResource</title>\r\n  <id>123</id>\r\n  <updated>10/10/2010 12:00:00 AM</updated>\r\n  <content><![CDATA[<SomeResource><Name>John Doe</Name><Amount>123.45</Amount><Id>123</Id><UpdatedAt>2010-10-10T00:00:00</UpdatedAt></SomeResource>]]></content>\r\n</entry>";
            
            Assert.AreEqual(expectedResult, atom);
        }

        [Test]
        public void ShouldSerializeAListOfResources()
        {
            var resources = new []
                                          {
                                              new SomeResource {Amount = 123.45, Name = "John Doe"},
                                              new SomeResource {Amount = 67.89, Name = "Sally Doe"}
                                          };

            var atom = serializer.Serialize(resources);

            Assert.That(atom.Contains("<feed xmlns=\"http://www.w3.org/2005/Atom\">"));
            Assert.That(atom.Contains("John Doe"));
            Assert.That(atom.Contains("Sally Doe"));
            Assert.That(atom.Contains("</feed>"));
        }

        private IList<Relation> SomeRelations()
        {
            return new List<Relation> { new Relation("pay", "http://some/url") };
        }
    }
}
