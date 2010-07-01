using Moq;
using NUnit.Framework;
using Restfulie.Server.Marshalling.UrlGenerators;

namespace Restfulie.Server.Tests
{
    [TestFixture]
    public class RelationsFactoryTests
    {
        [Test]
        public void ShouldCreateANewRelation()
        {
            var factory = new RelationsFactory(new Mock<IUrlGenerator>().Object);

            var firstRelation = factory.NewRelations();
            var secondRelation = factory.NewRelations();

            Assert.IsTrue(firstRelation != null);
            Assert.IsTrue(secondRelation != null);
            Assert.IsTrue(firstRelation != secondRelation);

        }
    }
}
