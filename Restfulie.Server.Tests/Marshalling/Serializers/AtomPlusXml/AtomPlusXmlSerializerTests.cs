using System;
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

            var atom = serializer.Serialize(resources);

            const string expectedAtomPart1 =
                "<feed xmlns=\"http://www.w3.org/2005/Atom\">\r\n  " +
                "<title>(title)</title>\r\n  " +
                "<updated>";
            
            const string expectedAtomPart2 = 
                "</updated>\r\n"+
                "  <author>\r\n"+
                "    <name>(author)</name>\r\n"+
                "  </author>\r\n"+
                "  <id>(feed-url)</id>\r\n"+
                "  <entry>\r\n"+
                "    <title>(title)</title>\r\n"+
                "    <id>(entry-url)</id>\r\n"+
                "    <updated>0001-01-01T00:00:00.000</updated>\r\n"+
                "    <content type=\"application/xml\" xmlns=\"\">\r\n"+
                "      <SomeResource>\r\n"+
                "        <Name>John Doe</Name>\r\n"+
                "        <Amount>123.45</Amount>\r\n"+
                "        <Id>0</Id>\r\n"+
                "        <UpdatedAt>0001-01-01T00:00:00</UpdatedAt>\r\n"+
                "      </SomeResource>\r\n"+
                "    </content>\r\n"+
                "  </entry>\r\n"+
                "  <entry>\r\n"+
                "    <title>(title)</title>\r\n"+
                "    <id>(entry-url)</id>\r\n"+
                "    <updated>0001-01-01T00:00:00.000</updated>\r\n"+
                "    <content type=\"application/xml\" xmlns=\"\">\r\n"+
                "      <SomeResource>\r\n"+
                "        <Name>Sally Doe</Name>\r\n"+
                "        <Amount>67.89</Amount>\r\n"+
                "        <Id>0</Id>\r\n"+
                "        <UpdatedAt>0001-01-01T00:00:00</UpdatedAt>\r\n"+
                "      </SomeResource>\r\n"+
                "    </content>\r\n"+
                "  </entry>\r\n"+
                "</feed>";

            Assert.IsTrue(atom.Contains(expectedAtomPart1));
            Assert.IsTrue(atom.Contains(expectedAtomPart2));
        }

        [Test]
        public void ShouldSerializeAResource()
        {
            var date = new DateTime(2010, 10, 10);
            var resource = new SomeResource {Name = "John Doe", Amount = 123.45, Id = 123, UpdatedAt = date};
            var atom = serializer.Serialize(resource);

            const string expectedResult =
                "<entry xmlns=\"http://www.w3.org/2005/Atom\">\r\n"+
                "  <title>(title)</title>\r\n"+
                "  <id>(entry-url)</id>\r\n"+
                "  <updated>2010-10-10T00:00:00.000</updated>\r\n"+
                "  <content type=\"application/xml\" xmlns=\"\">\r\n"+
                "    <SomeResource>\r\n"+
                "      <Name>John Doe</Name>\r\n"+
                "      <Amount>123.45</Amount>\r\n"+
                "      <Id>123</Id>\r\n"+
                "      <UpdatedAt>2010-10-10T00:00:00</UpdatedAt>\r\n"+
                "    </SomeResource>\r\n"+
                "  </content>\r\n"+
                "</entry>";

            Assert.AreEqual(expectedResult, atom);
        }
    }
}