using System.Collections.Generic;
using Moq;
using NUnit.Framework;
using Restfulie.Server.Marshalling.Serializers.AtomPlusXml;
using Restfulie.Server.Marshalling.UrlGenerators;
using Restfulie.Server.Request;

namespace Restfulie.Server.Tests.Marshalling.Serializers.AtomPlusXml
{
    [TestFixture]
    public class AtomPlusXmlHypermediaInserterTests
    {
        private Mock<IRequestInfoFinder> requestInfo;

        [SetUp]
        public void SetUp()
        {
            requestInfo = new Mock<IRequestInfoFinder>();
        }

        [Test]
        public void ShouldInsertTransitionsInAEntry()
        {
            const string entry = "<entry>\n" +
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

            var relations = new Mock<Relations>(new Mock<IUrlGenerator>().Object);
            relations.Setup(r => r.GetAll()).Returns(new List<Relation>
                                {
                                    new Relation("pay", "some/url")
                                });

            var result = new AtomPlusXmlHypermediaInserter().Insert(entry, relations.Object, requestInfo.Object);

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

            var relationsFor123 = new Mock<Relations>(new Mock<IUrlGenerator>().Object);
            var relationsFor456 = new Mock<Relations>(new Mock<IUrlGenerator>().Object);

            relationsFor123.Setup(r => r.GetAll()).Returns(new List<Relation>
                                {
                                    new Relation("pay", "some/url/123")
                                });

            relationsFor456.Setup(r => r.GetAll()).Returns(new List<Relation>
                                {
                                    new Relation("pay", "some/url/456")
                                });

            var result = new AtomPlusXmlHypermediaInserter().Insert(feed, new List<Relations> { relationsFor123.Object, relationsFor456.Object }, requestInfo.Object);


            Assert.AreEqual(
                "<feed xmlns=\"http://www.w3.org/2005/Atom\"><entry><title>some title</title><id>123</id><updated>05—01—2006 02:56:00</updated><summary>summary</summary><author><name>author</name></author><content><![CDATA[<SomeResource>\n<Name>Name</Name>\n<Amount>123.45</Amount>\n</SomeResource>\n]]></content><link rel=\"pay\" href=\"some/url/123\" xmlns=\"\" /></entry><entry><title>some title</title><id>456</id><updated>05—01—2006 02:56:00</updated><summary>summary</summary><author><name>author</name></author><content><![CDATA[<SomeResource>\n<Name>Name2</Name>\n<Amount>67.89</Amount>\n</SomeResource>\n]]></content><link rel=\"pay\" href=\"some/url/456\" xmlns=\"\" /></entry></feed>"
                , result);
        }
    }
}
