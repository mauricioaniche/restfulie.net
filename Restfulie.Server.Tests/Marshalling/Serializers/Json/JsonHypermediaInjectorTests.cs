using System;
using System.Collections.Generic;
using NUnit.Framework;
using Restfulie.Server.Marshalling.Serializers.Json;
using Moq;
using Restfulie.Server.Marshalling.UrlGenerators;
using Restfulie.Server.Request;

namespace Restfulie.Server.Tests.Marshalling.Serializers.Json
{
    [TestFixture]
    public class JsonHypermediaInjectorTests
    {
        private Mock<IRequestInfoFinder> requestInfo;

        [SetUp]
        public void Setup()
        {
            requestInfo = new Mock<IRequestInfoFinder>();
        }

        [Test]
        public void ShouldInjectTransitionsInResource()
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

            var result = new JsonHypermediaInjector().Inject(json, relations.Object, requestInfo.Object);

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
        public void ShouldInjectTransitionsInAListOfResources()
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

            var result = new JsonHypermediaInjector().Inject(json, new List<Relations> { relationsForJohnDoe.Object, relationsForSallyDoe.Object }, requestInfo.Object);

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

        [Test]
        public void ShouldInsertTransitionsInAListOfResourcesWhenJsonHasAnInternalArray()
        {
            var json =
                "[" +
                    "{" +
                        "\"Name\":\"John Doe\"," +
                        "\"Amount\":123.45," +
                        "\"Books\":" + 
                        "[" +
                            "{" +
                                "\"Title\":\"Title 01\"" + 
                            "}," +
                            "{" +
                                "\"Title\":\"Title 02\"" + 
                            "}" +
                        "]" +
                    "}," +
                    "{" +
                        "\"Name\":\"Sally Doe\"," +
                        "\"Amount\":67.89," +
                        "\"Books\":" +
                        "[" +
                            "{" +
                                "\"Title\":\"Title 03\"" +
                            "}," +
                            "{" +
                                "\"Title\":\"Title 04\"" +
                            "}" +
                        "]" +
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

            var result = new JsonHypermediaInjector().Inject(json, new List<Relations> { relationsForJohnDoe.Object, relationsForSallyDoe.Object }, requestInfo.Object);

            Assert.AreEqual(
                "[" +
                    "{" +
                        "\"Name\":\"John Doe\"," +
                        "\"Amount\":123.45," +
                        "\"Books\":" + 
                        "[" +
                            "{" +
                                "\"Title\":\"Title 01\"" +
                            "}," +
                            "{" +
                                "\"Title\":\"Title 02\"" +
                            "}" +
                        "]," +
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
                        "\"Books\":" +
                        "[" +
                            "{" +
                                "\"Title\":\"Title 03\"" +
                            "}," +
                            "{" +
                                "\"Title\":\"Title 04\"" +
                            "}" +
                        "]," +
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

        [Test]
        public void ShouldInjectTransitionsInAListOfResourcesEvenWhenNumberOfRelationsIsLessThanNumberOfResources()
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

            var result = new JsonHypermediaInjector().Inject(json, new List<Relations> { relationsForJohnDoe.Object }, requestInfo.Object);

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
                        "\"Amount\":67.89" +
                    "}" +
                "]"
                , result);
        }
    }
}
