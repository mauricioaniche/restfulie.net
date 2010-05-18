using System;
using System.Linq;
using Moq;
using NUnit.Framework;
using Restfulie.Server.Marshalling.UrlGenerators;
using Restfulie.Server.Tests.Fixtures;

namespace Restfulie.Server.Tests
{
    [TestFixture]
    public class RelationsTests
    {
        [Test]
        public void ShouldTransitToAControllerAction()
        {
            var urlGenerator = new Mock<IUrlGenerator>();
            urlGenerator.Setup(ug => ug.For("Some", "SomeSimpleAction")).Returns("http://Some/SomeSimpleAction");

            var relations = new Relations(urlGenerator.Object);
            relations.Named("pay").Uses<SomeController>().SomeSimpleAction();

            var all = relations.GetAll();

            Assert.AreEqual("pay", all.First().Name);
            Assert.AreEqual("Some", all.First().Controller);
            Assert.AreEqual("SomeSimpleAction", all.First().Action);
            Assert.AreEqual("http://Some/SomeSimpleAction", all.First().Url);
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void ShouldDetectWrongUseOfFluentAPI()
        {
            var relations = new Relations(new Mock<IUrlGenerator>().Object);
            relations.Uses<SomeController>().SomeSimpleAction();
        }

        [Test]
        public void ShouldWorkWhenUsingTheAPIFluentlyInARow()
        {
            var relations = new Relations(new Mock<IUrlGenerator>().Object);
            relations.Named("pay").Uses<SomeController>().SomeSimpleAction();
            relations.Named("cancel").Uses<SomeController>().SomeSimpleAction();

            var all = relations.GetAll();

            Assert.AreEqual(2, all.Count);
            Assert.IsNotNull(all.Where(t => t.Name == "pay").Single());
            Assert.IsNotNull(all.Where(t => t.Name == "cancel").Single());
        }
    }
}