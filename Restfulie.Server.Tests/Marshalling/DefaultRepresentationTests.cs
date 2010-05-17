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
        [Test]
        public void ShouldBuildRepresentation()
        {
            var transitions = new Mock<Relations>(new Mock<IUrlGenerator>().Object);
            var serializer = new Mock<IResourceSerializer>(MockBehavior.Strict);

            var resource = new SomeResource();

            transitions.SetupGet(t => t.All).Returns(SomeTransitions());
            serializer.Setup(s => s.Serialize(resource, It.IsAny<IList<Relation>>())).Returns(URL());

            var builder = new DefaultRepresentation(transitions.Object, serializer.Object);
            builder.Build(resource);
            
            transitions.VerifyAll();
            serializer.VerifyAll();
        }

        private List<Relation> SomeTransitions()
        {
            return new List<Relation> {new Relation("pay", "Order","Pay",URL())};
        }

        private string URL()
        {
            return "http://some-url-here/";
        }
    }
}