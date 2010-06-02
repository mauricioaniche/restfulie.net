using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Web.Mvc;
using Moq;
using NUnit.Framework;
using Restfulie.Server.Marshalling;
using Restfulie.Server.Marshalling.Serializers;
using Restfulie.Server.Marshalling.UrlGenerators;
using Restfulie.Server.Tests.Fixtures;
using Restfulie.Server.Exceptions;

namespace Restfulie.Server.Tests.Marshalling
{
    [TestFixture]
    public class DefaultResourceMarshallerTests
    {
        private Mock<Relations> relations;
        private Mock<IResourceSerializer> serializer;

        protected Mock<HttpResponseBase> response;
        protected Mock<HttpContextBase> http;
        protected Mock<ControllerContext> context;
        protected Mock<TextWriter> stream;

        protected void SetUpRequest()
        {
            response = new Mock<HttpResponseBase>();
            http = new Mock<HttpContextBase>();
            context = new Mock<ControllerContext>();

            http.Setup(h => h.Response).Returns(response.Object);
            context.Setup(c => c.HttpContext).Returns(http.Object);

            stream = new Mock<TextWriter>();
            response.Setup(p => p.Output).Returns(stream.Object);
        }

        [SetUp]
        public void SetUp()
        {
            relations = new Mock<Relations>(new Mock<IUrlGenerator>().Object);
            serializer = new Mock<IResourceSerializer>();

            SetUpRequest();
        }

        [Test]
        public void ShouldBuildResourceRepresentation()
        {
            var resource = new SomeResource();

            relations.Setup(t => t.GetAll()).Returns(SomeTransitions());
            serializer.Setup(s => s.Serialize(resource, It.IsAny<IList<Relation>>())).Returns(SerializedResource());

            var builder = new DefaultResourceMarshaller(relations.Object, serializer.Object);
            builder.Build(context.Object, new MarshallingInfo{Resource = resource});
            
            relations.VerifyAll();
            serializer.VerifyAll();
            stream.Verify(s => s.Write(It.IsAny<string>()));
        }

        [Test]
        public void ShouldBuildListRepresentation()
        {
            var resources = new List<IBehaveAsResource> {new SomeResource(), new SomeResource()};

            relations.Setup(t => t.GetAll()).Returns(SomeTransitions());
            serializer.Setup(s => s.Serialize(It.IsAny<IDictionary<IBehaveAsResource, IList<Relation>>>())).Returns(SerializedResource());
            
            var builder = new DefaultResourceMarshaller(relations.Object, serializer.Object);
            builder.Build(context.Object, new MarshallingInfo { Resources = resources});

            relations.VerifyAll();
            serializer.VerifyAll();
            stream.Verify(s => s.Write(It.IsAny<string>()));
        }

        [Test]
        public void ShouldBuildMessageRepresentation()
        {
            var builder = new DefaultResourceMarshaller(relations.Object, serializer.Object);
            builder.Build(context.Object, new MarshallingInfo { Message = "message" });

            relations.VerifyAll();
            stream.Verify(s => s.Write(It.Is<string>(msg => msg == "message")));
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