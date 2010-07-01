using System.Collections.Generic;
using Moq;
using NUnit.Framework;
using Restfulie.Server.Marshalling;
using Restfulie.Server.Marshalling.Serializers;
using Restfulie.Server.Marshalling.UrlGenerators;
using Restfulie.Server.Tests.Fixtures;

namespace Restfulie.Server.Tests.Marshalling
{
    [TestFixture]
    public class RestfulieMarshallerTests
    {
        private Mock<IRelationsFactory> relationsFactory;
        private Mock<Relations> relations;
        private Mock<IResourceSerializer> serializer;
        private Mock<IHypermediaInserter> hypermedia;

        [SetUp]
        public void SetUp()
        {
            relations = new Mock<Relations>(new Mock<IUrlGenerator>().Object);
            serializer = new Mock<IResourceSerializer>();
            hypermedia = new Mock<IHypermediaInserter>();
            relationsFactory = new Mock<IRelationsFactory>();

            relationsFactory.Setup(r => r.NewRelations()).Returns(relations.Object);
        }

        [Test]
        public void ShouldBuildResourceRepresentation()
        {
            var resource = new SomeResource();
            
            serializer.Setup(s => s.Serialize(resource)).Returns(SerializedResource());
            hypermedia.Setup(h => h.Insert(SerializedResource(), relations.Object)).Returns(HypermediaResource());

            var builder = new RestfulieMarshaller(relationsFactory.Object, serializer.Object, hypermedia.Object);
            var representation = builder.Build(resource);

            serializer.VerifyAll();
            hypermedia.VerifyAll();

            Assert.AreEqual(HypermediaResource(), representation);
        }

        [Test]
        public void ShouldBuildListRepresentation()
        {
            var resources = new List<IBehaveAsResource> { new SomeResource(), new SomeResource() };
            
            hypermedia.Setup(h => h.Insert(SerializedListOfResources(), It.IsAny<IList<Relations>>())).Returns(
                SerializedHypermediaList());
            serializer.Setup(s => s.Serialize(resources)).Returns(SerializedListOfResources());

            var builder = new RestfulieMarshaller(relationsFactory.Object, serializer.Object, hypermedia.Object);
            var representation = builder.Build(resources);

            serializer.VerifyAll();
            hypermedia.VerifyAll();
            Assert.AreEqual(representation, SerializedHypermediaList());
        }

        [Test]
        public void ShouldBuildANonResource()
        {
            var model = new List<int>();

            serializer.Setup(s => s.Serialize(model)).Returns("some list of integers");

            var builder = new RestfulieMarshaller(relationsFactory.Object, serializer.Object, hypermedia.Object);
            var representation = builder.Build(model);

            serializer.VerifyAll();
            Assert.AreEqual("some list of integers", representation);
        }

        private static string SerializedResource()
        {
            return "resource here";
        }

        private string HypermediaResource()
        {
            return "hypermedia resource";
        }

        private static string SerializedListOfResources()
        {
            return "resource1 here, resource 2 here";
        }

        private static string SerializedHypermediaList()
        {
            return "hypermedia resource list";
        }
    }
}