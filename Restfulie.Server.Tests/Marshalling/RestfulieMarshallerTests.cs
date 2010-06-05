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
        private Mock<Relations> relations;
        private Mock<IResourceSerializer> serializer;
        private Mock<IHypermediaInserter> hypermedia;

        [SetUp]
        public void SetUp()
        {
            relations = new Mock<Relations>(new Mock<IUrlGenerator>().Object);
            serializer = new Mock<IResourceSerializer>();
            hypermedia = new Mock<IHypermediaInserter>();
        }

        [Test]
        public void ShouldBuildResourceRepresentation()
        {
            var resource = new SomeResource();

            relations.Setup(t => t.GetAll()).Returns(SomeTransitions());
            serializer.Setup(s => s.Serialize(resource)).Returns(SerializedResource());

            var builder = new RestfulieMarshaller(relations.Object, serializer.Object, hypermedia.Object);
            var representation = builder.Build(resource);

            relations.VerifyAll();
            serializer.VerifyAll();
            Assert.AreEqual(representation, SerializedResource());
        }

        [Test]
        [Ignore]
        public void ShouldBuildListRepresentation()
        {
            //var resources = new List<IBehaveAsResource> { new SomeResource(), new SomeResource() };

            //relations.Setup(t => t.GetAll()).Returns(SomeTransitions());
            //serializer.Setup(s => s.Serialize(It.IsAny<IDictionary<IBehaveAsResource, IList<Relation>>>())).Returns(SerializedListOfResources());

            //var builder = new RestfulieMarshaller(relations.Object, serializer.Object);
            //var representation = builder.Build(resources);

            //relations.VerifyAll();
            //serializer.VerifyAll();
            //Assert.AreEqual(representation, SerializedListOfResources());
        }

        private List<Relation> SomeTransitions()
        {
            return new List<Relation> { new Relation("pay", "Order", "Pay", new Dictionary<string, object>(), SerializedResource()) };
        }

        private static string SerializedResource()
        {
            return "resource here";
        }

        private static string SerializedListOfResources()
        {
            return "resource1 here, resource 2 here";
        }
    }
}