using System;
using System.Collections.Generic;
using Moq;
using NUnit.Framework;
using Restfulie.Server.ResourceRepresentation;
using Restfulie.Server.ResourceRepresentation.Serializers;
using Restfulie.Server.ResourceRepresentation.UrlGenerators;
using Restfulie.Server.Tests.Fixtures;

namespace Restfulie.Server.Tests.ResourceRepresentation
{
    [TestFixture]
    public class XmlAndHypermediaRepresentationTests
    {
        [Test]
        public void ShouldBuildRepresentation()
        {
            var urlGenerator = new Mock<IUrlGenerator>(MockBehavior.Strict);
            var serializer = new Mock<ISerializer>(MockBehavior.Strict);

            var resource = new SomeResource();

            urlGenerator.Setup(ug => ug.For("Some", "SomeSimpleAction")).Returns("http://Some/SomeSimpleAction");
            serializer.Setup(s => s.Serialize(resource, It.IsAny<IList<Transition>>())).Returns("some url here");

            var builder = new XMLAndHypermediaRepresentation(urlGenerator.Object, serializer.Object);
            builder.Build(resource);
            
            urlGenerator.VerifyAll();
            serializer.VerifyAll();
        }
    }
}
