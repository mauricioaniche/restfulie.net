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
    public class DefaultRepresentationTests
    {
        private Mock<Relations> relations;
        private Mock<IResourceSerializer> serializer;
        private Mock<IInflections> inflections;

        [SetUp]
        public void SetUp()
        {
            relations = new Mock<Relations>(new Mock<IUrlGenerator>().Object);
            serializer = new Mock<IResourceSerializer>(MockBehavior.Strict);
            inflections = new Mock<IInflections>(MockBehavior.Strict);

            inflections.Setup(i => i.Inflect("SomeResource")).Returns("SomeResources");
        }

        [Test]
        public void ShouldBuildResourceRepresentation()
        {
            var resource = new SomeResource();

            relations.SetupGet(t => t.All).Returns(SomeTransitions());
            serializer.Setup(s => s.Serialize(resource, It.IsAny<IList<Relation>>())).Returns(SerializedResource());

            var builder = new DefaultRepresentation(relations.Object, serializer.Object, inflections.Object);
            builder.Build(resource);
            
            relations.VerifyAll();
            serializer.VerifyAll();
        }

        [Test]
        public void ShouldBuildListRepresentation()
        {
            var resources = new List<IBehaveAsResource> {new SomeResource(), new SomeResource()};

            relations.Setup(t => t.All).Returns(SomeTransitions());
            serializer.Setup(s => s.Serialize(It.IsAny<IDictionary<IBehaveAsResource, IList<Relation>>>(), "SomeResources")).Returns(SerializedResource());
            
            var builder = new DefaultRepresentation(relations.Object, serializer.Object, inflections.Object);
            builder.Build(resources);

            relations.VerifyAll();
            serializer.VerifyAll();
        }

        private List<Relation> SomeTransitions()
        {
            return new List<Relation> {new Relation("pay", "Order","Pay",SerializedResource())};
        }

        private string SerializedResource()
        {
            return "resource here";
        }
    }
}