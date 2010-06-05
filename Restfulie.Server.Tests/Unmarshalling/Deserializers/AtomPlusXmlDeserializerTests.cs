using NUnit.Framework;
using Restfulie.Server.Tests.Fixtures;
using Restfulie.Server.Unmarshalling.Deserializers.AtomPlusXml;

namespace Restfulie.Server.Tests.Unmarshalling.Deserializers
{
    [TestFixture]
    public class AtomPlusXmlDeserializerTests
    {
        [Test]
        public void ShouldDeserializeAnEntry()
        {
            var entry =
                "<entry>\n" +
                    "<title>some title</title>\n" +
                    "<link href=\"/some/link\"/>\n" +
                    "<id>1234</id>\n" +
                    "<updated>05—01—2006 02:56:00</updated>\n" +
                    "<summary>summary</summary>\n" +
                    "<author>\n" +
                        "<name>author</name>\n" +
                    "</author>\n" +
                    "<content>\n" +
                        "<![CDATA["+
                            "<SomeResource>\n"+
                                "<Name>Name</Name>\n"+
                                "<Amount>123.45</Amount>\n"+
                            "</SomeResource>\n" +
                        "]]>"+
                    "</content>\n" +
                "</entry> ";

            var serializer = new AtomPlusXmlDeserializer();
            var resource = serializer.Deserialize(entry, typeof (SomeResource)) as SomeResource;

            Assert.AreEqual("Name", resource.Name);
            Assert.AreEqual(123.45, resource.Amount);
        }

        [Test]
        public void ShouldDeserializeAnFeed()
        {
            var feed =
                "<feed xmlns=\"http://www.w3.org/2005/Atom\">" +
                    "<entry>\n" +
                        "<title>some title</title>\n" +
                        "<link href=\"/some/link\"/>\n" +
                        "<id>1234</id>\n" +
                        "<updated>05—01—2006 02:56:00</updated>\n" +
                        "<summary>summary</summary>\n" +
                        "<author>\n" +
                            "<name>author</name>\n" +
                        "</author>\n" +
                        "<content>\n" +
                            "<![CDATA[" +
                                "<SomeResource>\n" +
                                    "<Name>Name</Name>\n" +
                                    "<Amount>123.45</Amount>\n" +
                                "</SomeResource>\n" +
                            "]]>" +
                        "</content>\n" +
                    "</entry> "+

                    "<entry>\n" +
                        "<title>some title</title>\n" +
                        "<link href=\"/some/link\"/>\n" +
                        "<id>1234</id>\n" +
                        "<updated>05—01—2006 02:56:00</updated>\n" +
                        "<summary>summary</summary>\n" +
                        "<author>\n" +
                            "<name>author</name>\n" +
                        "</author>\n" +
                        "<content>\n" +
                            "<![CDATA[" +
                                "<SomeResource>\n" +
                                    "<Name>Name2</Name>\n" +
                                    "<Amount>67.89</Amount>\n" +
                                "</SomeResource>\n" +
                            "]]>" +
                        "</content>\n" +
                    "</entry> " +
                "</feed>";

            var serializer = new AtomPlusXmlDeserializer();
            var resources = (SomeResource[])serializer.Deserialize(feed, typeof(SomeResource[]));

            Assert.AreEqual("Name", resources[0].Name);
            Assert.AreEqual(123.45, resources[0].Amount);
            Assert.AreEqual("Name2", resources[1].Name);
            Assert.AreEqual(67.89, resources[1].Amount);
        }
    }
}
