using System.Collections.Generic;
using NUnit.Framework;
using Restfulie.Server.Marshalling.Serializers.XmlAndHypermedia;

namespace Restfulie.Server.Tests.Marshalling.Serializers.XmlAndHypermedia
{
    [TestFixture]
    public class XmlHypermediaInserterTests
    {
        [Test]
        public void ShouldInsertTransitionsInResource()
        {
            var content = "<SomeResource><Name>123</Name></SomeResource>";
            var relations = new List<Relation>
                                {
                                    new Relation("pay", "controller", "action", new Dictionary<string, object>(),
                                                 "some/url")
                                };

            var result = new XmlHypermediaInserter().Insert(content, relations);

            Assert.AreEqual("<SomeResource><Name>123</Name><atom:link rel=\"pay\" href=\"some/url\" xmlns:atom=\"http://www.w3.org/2005/Atom\" /></SomeResource>", result);
        }

        [Test]
        public void ShouldInsertTransitionsInAListOfResources()
        {
            var content =
                "<SomeResources>" +
                    "<SomeResource><Name>123</Name></SomeResource>" +
                    "<SomeResource><Name>456</Name></SomeResource>" +
                "</SomeResources>";

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

            var result = new XmlHypermediaInserter().Insert(content, new List<IList<Relation>> { relationsFor123, relationsFor456 });

            Assert.AreEqual(
                "<SomeResources>"+
                    "<SomeResource><Name>123</Name><atom:link rel=\"pay\" href=\"some/url/123\" xmlns:atom=\"http://www.w3.org/2005/Atom\" /></SomeResource>"+
                    "<SomeResource><Name>456</Name><atom:link rel=\"pay\" href=\"some/url/456\" xmlns:atom=\"http://www.w3.org/2005/Atom\" /></SomeResource>" +
                "</SomeResources>", 
                result);
        }
    }
}
