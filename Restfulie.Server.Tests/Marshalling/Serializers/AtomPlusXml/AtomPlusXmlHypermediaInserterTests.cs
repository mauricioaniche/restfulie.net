using System.Collections.Generic;
using NUnit.Framework;
using Restfulie.Server.Marshalling.Serializers.AtomPlusXml;

namespace Restfulie.Server.Tests.Marshalling.Serializers.AtomPlusXml
{
    [TestFixture]
    public class AtomPlusXmlHypermediaInserterTests
    {
        [Test]
        public void ShouldInsertTransitionsInAEntry()
        {
            var entry =
                "<entry>\n" +
                    "<title>some title</title>\n" +
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
                "</entry> ";

            var relations = new List<Relation>
                                {
                                    new Relation("pay", "controller", "action", new Dictionary<string, object>(),
                                                 "some/url")
                                };

            var result = new AtomPlusXmlHypermediaInserter().Insert(entry, relations);

            Assert.AreEqual(
                "<entry>"+
                "<title>some title</title>"+
                "<id>1234</id>"+
                "<updated>05—01—2006 02:56:00</updated>"+
                "<summary>summary</summary>"+
                "<author>"+
                "<name>author</name>"+
                "</author>"+
                "<content><![CDATA[<SomeResource>\n<Name>Name</Name>\n<Amount>123.45</Amount>\n</SomeResource>\n]]></content>"+
                "<link rel=\"pay\" href=\"some/url\" />"+
                "</entry>"
                , result);
        }

        [Test]
        public void ShouldInsertTransitionsInAFeed()
        {
            var feed =
                "<feed xmlns=\"http://www.w3.org/2005/Atom\">" +
                    "<entry>\n" +
                        "<title>some title</title>\n" +
                        "<id>123</id>\n" +
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
                    "</entry> " +

                    "<entry>\n" +
                        "<title>some title</title>\n" +
                        "<id>456</id>\n" +
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

            var relationsFor123 = new List<Relation>
                                {
                                    new Relation("pay", "controller", "action", new Dictionary<string, object>(),
                                                 "some/url/123")
                                };

            var relationsFor456 = new List<Relation>
                                {
                                    new Relation("pay", "controller", "action", new Dictionary<string, object>(),
                                                 "some/url/456")
                                };

            var result = new AtomPlusXmlHypermediaInserter().Insert(feed, new List<IList<Relation>> { relationsFor123, relationsFor456 });


            Assert.AreEqual(
                "<feed xmlns=\"http://www.w3.org/2005/Atom\"><entry><title>some title</title><id>123</id><updated>05—01—2006 02:56:00</updated><summary>summary</summary><author><name>author</name></author><content><![CDATA[<SomeResource>\n<Name>Name</Name>\n<Amount>123.45</Amount>\n</SomeResource>\n]]></content><link rel=\"pay\" href=\"some/url/123\" xmlns=\"\" /></entry><entry><title>some title</title><id>456</id><updated>05—01—2006 02:56:00</updated><summary>summary</summary><author><name>author</name></author><content><![CDATA[<SomeResource>\n<Name>Name2</Name>\n<Amount>67.89</Amount>\n</SomeResource>\n]]></content><link rel=\"pay\" href=\"some/url/456\" xmlns=\"\" /></entry></feed>"
                , result);
        }
    }
}
