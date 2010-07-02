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
        public void ShouldSerializeAListOfResources()
        {
            var resources = new[]
                                {
                                    new SomeResource {Amount = 123.45, Name = "John Doe"},
                                    new SomeResource {Amount = 67.89, Name = "Sally Doe"}
                                };

            string atom = serializer.Serialize(resources);

            Assert.That(atom.Contains("<feed xmlns=\"http://www.w3.org/2005/Atom\">"));
            Assert.That(atom.Contains("John Doe"));
            Assert.That(atom.Contains("Sally Doe"));
            Assert.That(atom.Contains("</feed>"));
        }

        [Test]
        public void ShouldSerializeAResource()
        {
            var date = new DateTime(2010, 10, 10);
            var resource = new SomeResource {Name = "John Doe", Amount = 123.45, Id = 123, UpdatedAt = date};
            var atom = serializer.Serialize(resource);

            const string expectedResult =
                "<entry xmlns=\"http://www.w3.org/2005/Atom\">\r\n  "+
                    "<title>(title)</title>\r\n  "+
                    "<id>(entry-url)</id>\r\n  "+
                    "<updated>2010-10-10T00:00:00.000</updated>\r\n  "+
                    "<content>\r\n    "+
                        "<SomeResource xmlns=\"\">\r\n      "+
                            "<Name>John Doe</Name>\r\n      "+
                            "<Amount>123.45</Amount>\r\n      "+
                            "<Id>123</Id>\r\n      "+
                            "<UpdatedAt>2010-10-10T00:00:00</UpdatedAt>\r\n    "+
                        "</SomeResource>\r\n  "+
                    "</content>\r\n"+
                "</entry>";

            Assert.AreEqual(expectedResult, atom);
        }
    }
}