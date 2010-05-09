using System;
using System.Linq;
using Moq;
using NUnit.Framework;
using Restfulie.Server.UrlGenerators;

namespace Restfulie.Server.Tests.Transitions
{
    [TestFixture]
    public class TransitionsTests
    {
        [Test]
        public void ShouldTransitToAControllerAction()
        {
            var urlGenerator = BuildUrlGenerator();

            var transit = new Server.Transitions(urlGenerator.Object);
            transit.Named("pay").Uses<SomeController>().SomeSimpleAction();

            Assert.AreEqual("pay", transit.All.First().Name);
            Assert.AreEqual("http://Some/SomeSimpleAction", transit.All.First().Url);
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void ShouldDetectWrongUseOfFluentAPI()
        {
            var transit = new Server.Transitions(new Mock<IUrlGenerator>().Object);
            transit.Uses<SomeController>().SomeSimpleAction();
        }

        [Test]
        public void ShouldWorkWhenUsingTheAPIFluently()
        {
            var urlGenerator = BuildUrlGenerator();

            var transit = new Server.Transitions(urlGenerator.Object);
            transit.Named("pay").Uses<SomeController>().SomeSimpleAction();
            transit.Named("cancel").Uses<SomeController>().SomeSimpleAction();

            Assert.AreEqual(2, transit.All.Count);
            Assert.IsNotNull(transit.All.Where(t => t.Name == "pay").Single());
            Assert.IsNotNull(transit.All.Where(t => t.Name == "cancel").Single());
        }

        private Mock<IUrlGenerator> BuildUrlGenerator()
        {
            var urlGenerator = new Mock<IUrlGenerator>(MockBehavior.Strict);
            urlGenerator.Setup(p => p.For("SomeSimpleAction", "Some")).Returns("http://Some/SomeSimpleAction");
            return urlGenerator;
        }
    }
}
