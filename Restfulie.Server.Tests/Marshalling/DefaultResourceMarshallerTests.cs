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
    public class DefaultResourceMarshallerTests
    {
        private Mock<Relations> relations;
        private Mock<IResourceSerializer> serializer;

        [SetUp]
        public void SetUp()
        {
            relations = new Mock<Relations>(new Mock<IUrlGenerator>().Object);
            serializer = new Mock<IResourceSerializer>(MockBehavior.Strict);
        }

        [Test]
        public void ShouldBuildResourceRepresentation()
        {
            var resource = new SomeResource();

            relations.Setup(t => t.GetAll()).Returns(SomeTransitions());
            serializer.Setup(s => s.Serialize(resource, It.IsAny<IList<Relation>>())).Returns(SerializedResource());

            var builder = new DefaultResourceMarshaller(relations.Object, serializer.Object);
            builder.Build(resource);
            
            relations.VerifyAll();
            serializer.VerifyAll();
        }

        [Test]
        public void ShouldBuildListRepresentation()
        {
            var resources = new List<IBehaveAsResource> {new SomeResource(), new SomeResource()};

            relations.Setup(t => t.GetAll()).Returns(SomeTransitions());
            serializer.Setup(s => s.Serialize(It.IsAny<IDictionary<IBehaveAsResource, IList<Relation>>>())).Returns(SerializedResource());
            
            var builder = new DefaultResourceMarshaller(relations.Object, serializer.Object);
            builder.Build(resources);

            relations.VerifyAll();
            serializer.VerifyAll();
        }

        [Test]
        public void ShouldReturnMediaTypeBasedOnSerializerFormat()
        {
            serializer.SetupGet(s => s.Format).Returns("format");
            var builder = new DefaultResourceMarshaller(relations.Object, serializer.Object);
   
            Assert.AreEqual("format", builder.MediaType);
        }

        private List<Relation> SomeTransitions()
        {
            return new List<Relation> {new Relation("pay", "Order","Pay", new Dictionary<string, object>(), SerializedResource())};
        }

        private string SerializedResource()
        {
            return "resource here";
        }
    }
}