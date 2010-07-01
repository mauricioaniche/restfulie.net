using System;
using System.Collections.Generic;
using NUnit.Framework;
using Restfulie.Server.Marshalling.Serializers.Json;
using Moq;
using Restfulie.Server.Marshalling.UrlGenerators;

namespace Restfulie.Server.Tests.Marshalling.Serializers.Json
{
    [TestFixture]
    public class JsonHypermediaInserterTests
    {
        [Test]
        public void ShouldInsertTransitionsInResource()
        {
            var json = 
                "{" +
                    "\"Name\":\"John Doe\"," +
                    "\"Amount\":123.45" +
                "}";

            var relations = new Mock<Relations>(new Mock<IUrlGenerator>().Object);
            relations.Setup(r => r.GetAll()).Returns(new List<Relation>
                                                         {
                                                            new Relation("pay", "http://some/url/pay/john-doe"),
                                                            new Relation("refresh", "http://some/url/refresh/john-doe")
                                                         });

            var result = new JsonHypermediaInserter().Insert(json, relations.Object);

            Assert.AreEqual(
                "{" +
                    "\"Name\":\"John Doe\"," +
                    "\"Amount\":123.45," +
                    "\"links\":" + 
                        "[" +
                            "{" + 
                                "\"rel\":\"pay\"," +
                                "\"href\":\"http://some/url/pay/john-doe\"" +
                            "}," +
                            "{" +
                                "\"rel\":\"refresh\"," +
                                "\"href\":\"http://some/url/refresh/john-doe\"" +
                            "}" +
                        "]" +
                "}"
                , result);
        }

        [Test]
        public void ShouldInsertTransitionsInAListOfResources()
        {
            var json =
                "[" +
                    "{" +
                        "\"Name\":\"John Doe\"," +
                        "\"Amount\":123.45" +
                    "}," +
                    "{" +
                        "\"Name\":\"Sally Doe\"," +
                        "\"Amount\":67.89" +
                    "}" +
                "]";


            var relationsForJohnDoe = new Mock<Relations>(new Mock<IUrlGenerator>().Object);
            var relationsForSallyDoe = new Mock<Relations>(new Mock<IUrlGenerator>().Object);

            relationsForJohnDoe.Setup(r => r.GetAll()).Returns(new List<Relation>
                                {
                                    new Relation("pay", "http://some/url/pay/john-doe"),
                                    new Relation("refresh", "http://some/url/refresh/john-doe")
                                });

            relationsForSallyDoe.Setup(r => r.GetAll()).Returns(new List<Relation>
                                {
                                    new Relation("pay", "http://some/url/pay/sally-doe"),
                                    new Relation("refresh", "http://some/url/refresh/sally-doe")
                                });

            var result = new JsonHypermediaInserter().Insert(json, new List<Relations> { relationsForJohnDoe.Object, relationsForSallyDoe.Object });

            Assert.AreEqual(
                "[" +
                    "{" +
                        "\"Name\":\"John Doe\"," +
                        "\"Amount\":123.45," +
                        "\"links\":" +
                            "[" +
                                "{" +
                                    "\"rel\":\"pay\"," +
                                    "\"href\":\"http://some/url/pay/john-doe\"" +
                                "}," +
                                "{" +
                                    "\"rel\":\"refresh\"," +
                                    "\"href\":\"http://some/url/refresh/john-doe\"" +
                                "}" +
                            "]" +
                    "}," +
                    "{" +
                        "\"Name\":\"Sally Doe\"," +
                        "\"Amount\":67.89," +
                        "\"links\":" +
                            "[" +
                                "{" +
                                    "\"rel\":\"pay\"," +
                                    "\"href\":\"http://some/url/pay/sally-doe\"" +
                                "}," +
                                "{" +
                                    "\"rel\":\"refresh\"," +
                                    "\"href\":\"http://some/url/refresh/sally-doe\"" +
                                "}" +
                            "]" +
                    "}" +
                "]"
                , result);
        }
    }
}
