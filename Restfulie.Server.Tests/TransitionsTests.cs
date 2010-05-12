using System;
using System.Linq;
using Moq;
using NUnit.Framework;
using Restfulie.Server.Marshalling.UrlGenerators;
using Restfulie.Server.Tests.Fixtures;

namespace Restfulie.Server.Tests
{
    [TestFixture]
    public class TransitionsTests
    {
        [Test]
        public void ShouldTransitToAControllerAction()
        {
            var urlGenerator = new Mock<IUrlGenerator>();
            urlGenerator.Setup(ug => ug.For("Some", "SomeSimpleAction")).Returns("http://Some/SomeSimpleAction");

            var transit = new Server.Transitions(urlGenerator.Object);
            transit.Named("pay").Uses<SomeController>().SomeSimpleAction();

            Assert.AreEqual("pay", transit.All.First().Name);
            Assert.AreEqual("Some", transit.All.First().Controller);
            Assert.AreEqual("SomeSimpleAction", transit.All.First().Action);
            Assert.AreEqual("http://Some/SomeSimpleAction", transit.All.First().Url);
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void ShouldDetectWrongUseOfFluentAPI()
        {
            var transit = new Transitions(new Mock<IUrlGenerator>().Object);
            transit.Uses<SomeController>().SomeSimpleAction();
        }

        [Test]
        public void ShouldWorkWhenUsingTheAPIFluentlyInARow()
        {
            var transit = new Transitions(new Mock<IUrlGenerator>().Object);
            transit.Named("pay").Uses<SomeController>().SomeSimpleAction();
            transit.Named("cancel").Uses<SomeController>().SomeSimpleAction();

            Assert.AreEqual(2, transit.All.Count);
            Assert.IsNotNull(transit.All.Where(t => t.Name == "pay").Single());
            Assert.IsNotNull(transit.All.Where(t => t.Name == "cancel").Single());
        }
    }
}