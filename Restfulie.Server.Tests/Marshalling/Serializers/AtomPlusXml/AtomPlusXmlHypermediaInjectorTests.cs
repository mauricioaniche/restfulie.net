using System.Collections.Generic;
using Moq;
using NUnit.Framework;
using Restfulie.Server.Http;
using Restfulie.Server.Marshalling.Serializers.AtomPlusXml;
using Restfulie.Server.Marshalling.UrlGenerators;

namespace Restfulie.Server.Tests.Marshalling.Serializers.AtomPlusXml
{
    [TestFixture]
    public class AtomPlusXmlHypermediaInjectorTests
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
            const string entry = 
                "<entry xmlns=\"http://www.w3.org/2005/Atom\">\r\n" +
                "  <title>(title)</title>\r\n" +
                "  <id>(entry-url)</id>\r\n" +
                "  <updated>2010-10-10T00:00:00.000</updated>\r\n" +
                "  <content type=\"application/xml\" xmlns=\"\">\r\n" +
                "    <SomeResource>\r\n" +
                "      <Name>John Doe</Name>\r\n" +
                "      <Amount>123.45</Amount>\r\n" +
                "      <Id>123</Id>\r\n" +
                "      <UpdatedAt>2010-10-10T00:00:00</UpdatedAt>\r\n" +
                "    </SomeResource>\r\n" +
                "  </content>\r\n" +
                "</entry>";

            var relations = new Mock<Relations>(new Mock<IUrlGenerator>().Object);
            relations.Setup(r => r.GetAll()).Returns(new List<Relation>
                                {
                                    new Relation("pay", "some/url"),
                                    new Relation("self", "some/get/url"),
                                });

            var result = new AtomPlusXmlHypermediaInjector().Inject(entry, relations.Object, requestInfo.Object);

            Assert.AreEqual(
               "<entry xmlns=\"http://www.w3.org/2005/Atom\">"+
               "<title>(title)</title>"+
               "<id>some/get/url</id>"+
               "<updated>2010-10-10T00:00:00.000</updated>"+
               "<content type=\"application/xml\" xmlns=\"\">"+
               "<SomeResource>"+
               "<Name>John Doe</Name>"+
               "<Amount>123.45</Amount>"+
               "<Id>123</Id>"+
               "<UpdatedAt>2010-10-10T00:00:00</UpdatedAt>"+
               "</SomeResource>"+
               "</content>"+
               "<link rel=\"pay\" href=\"some/url\" />"+
               "<link rel=\"self\" href=\"some/get/url\" />"+
               "</entry>", 
               result);
        }

        [Test]
        public void ShouldInsertTransitionsInAFeed()
        {
            const string feed =
                "<feed xmlns=\"http://www.w3.org/2005/Atom\">\r\n  " +
                "<title>(title)</title>\r\n  " +
                "<updated>0001-01-01T00:00:00</updated>" +
                "\r\n  <author>\r\n    " +
                "<name>(author)</name>\r\n  " +
                "</author>\r\n  " +
                "<id>(feed-url)</id>\r\n  " +
                "<entry>\r\n    " +
                "<title>(title)</title>\r\n    " +
                "<id>(entry-url)</id>\r\n    " +
                "<updated>0001-01-01T00:00:00.000</updated>\r\n    " +
                "<content>\r\n      " +
                "<SomeResource xmlns=\"\">\r\n        " +
                "<Name>John Doe</Name>\r\n        " +
                "<Amount>123.45</Amount>\r\n        " +
                "<Id>0</Id>\r\n        " +
                "<UpdatedAt>0001-01-01T00:00:00</UpdatedAt>\r\n      " +
                "</SomeResource>\r\n    " +
                "</content>\r\n  " +
                "</entry>\r\n  " +
                "<entry>\r\n    " +
                "<title>(title)</title>\r\n    " +
                "<id>(entry-url)</id>\r\n    " +
                "<updated>0001-01-01T00:00:00.000</updated>\r\n    " +
                "<content>\r\n      " +
                "<SomeResource xmlns=\"\">\r\n        " +
                "<Name>Sally Doe</Name>\r\n        " +
                "<Amount>67.89</Amount>\r\n        " +
                "<Id>0</Id>\r\n        " +
                "<UpdatedAt>0001-01-01T00:00:00</UpdatedAt>\r\n      " +
                "</SomeResource>\r\n    " +
                "</content>\r\n  " +
                "</entry>\r\n" +
                "</feed>";

            const string expectedFeed =
                "<feed xmlns=\"http://www.w3.org/2005/Atom\"><title>(title)</title><updated>0001-01-01T00:00:00</updated><author><name>(author)</name></author><id>entry/point</id><entry><title>(title)</title><id>some/get/url/123</id><updated>0001-01-01T00:00:00.000</updated><content><SomeResource xmlns=\"\"><Name>John Doe</Name><Amount>123.45</Amount><Id>0</Id><UpdatedAt>0001-01-01T00:00:00</UpdatedAt></SomeResource></content><link rel=\"pay\" href=\"some/url/123\" /><link rel=\"self\" href=\"some/get/url/123\" /></entry><entry><title>(title)</title><id>some/get/url/456</id><updated>0001-01-01T00:00:00.000</updated><content><SomeResource xmlns=\"\"><Name>Sally Doe</Name><Amount>67.89</Amount><Id>0</Id><UpdatedAt>0001-01-01T00:00:00</UpdatedAt></SomeResource></content><link rel=\"pay\" href=\"some/url/456\" /><link rel=\"self\" href=\"some/get/url/456\" /></entry></feed>";

            var relationsFor123 = new Mock<Relations>(new Mock<IUrlGenerator>().Object);
            var relationsFor456 = new Mock<Relations>(new Mock<IUrlGenerator>().Object);

            relationsFor123.Setup(r => r.GetAll()).Returns(new List<Relation>
                                {
                                    new Relation("pay", "some/url/123"),
                                    new Relation("self", "some/get/url/123")
                                });

            relationsFor456.Setup(r => r.GetAll()).Returns(new List<Relation>
                                {
                                    new Relation("pay", "some/url/456"),
                                    new Relation("self", "some/get/url/456")
                                });

            requestInfo.Setup(r => r.GetUrl()).Returns("entry/point");

            var result = new AtomPlusXmlHypermediaInjector().Inject(feed, new List<Relations> { relationsFor123.Object, relationsFor456.Object }, requestInfo.Object);

            Assert.AreEqual(expectedFeed, result);
            requestInfo.VerifyAll();
        }
    }
}
