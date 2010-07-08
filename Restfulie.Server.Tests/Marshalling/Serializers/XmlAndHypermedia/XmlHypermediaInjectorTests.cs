using System.Collections.Generic;
using Moq;
using NUnit.Framework;
using Restfulie.Server.Http;
using Restfulie.Server.Marshalling.Serializers.XmlAndHypermedia;
using Restfulie.Server.Marshalling.UrlGenerators;

namespace Restfulie.Server.Tests.Marshalling.Serializers.XmlAndHypermedia
{
    [TestFixture]
    public class XmlHypermediaInjectorTests
    {
        private IRequestInfoFinder requestInfo;

        [SetUp]
        public void SetUp()
        {
            requestInfo = new Mock<IRequestInfoFinder>().Object;
        }

        [Test]
        public void ShouldInsertTransitionsInResource()
        {
            var content = "<SomeResource><Name>123</Name></SomeResource>";

            var relations = new Mock<Relations>(new Mock<IUrlGenerator>().Object);
            relations.Setup(r => r.GetAll()).Returns(new List<Relation>
                                                         {
                                                             new Relation("pay", "some/url")
                                                         });

            var result = new XmlHypermediaInjector().Inject(content, relations.Object, requestInfo);

            Assert.AreEqual(
                "<SomeResource>"+
                "<Name>123</Name>"+
                "<atom:link rel=\"pay\" href=\"some/url\" xmlns:atom=\"http://www.w3.org/2005/Atom\" />"+
                "</SomeResource>", result);
        }

        [Test]
        public void ShouldInsertTransitionsInAListOfResources()
        {
            var content =
                "<SomeResources>" +
                    "<SomeResource><Name>123</Name></SomeResource>" +
                    "<SomeResource><Name>456</Name></SomeResource>" +
                "</SomeResources>";

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

            var result = new XmlHypermediaInjector().Inject(content, new List<Relations> { relationsFor123.Object, relationsFor456.Object }, requestInfo);

            Assert.AreEqual(
                "<SomeResources>"+
                    "<SomeResource><Name>123</Name><atom:link rel=\"pay\" href=\"some/url/123\" xmlns:atom=\"http://www.w3.org/2005/Atom\" /></SomeResource>"+
                    "<SomeResource><Name>456</Name><atom:link rel=\"pay\" href=\"some/url/456\" xmlns:atom=\"http://www.w3.org/2005/Atom\" /></SomeResource>" +
                "</SomeResources>", 
                result);
        }
    }
}
