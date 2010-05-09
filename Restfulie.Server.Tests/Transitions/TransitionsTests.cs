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
            var urlGenerator = new Mock<IUrlGenerator>(MockBehavior.Strict);
            urlGenerator.Setup(p => p.For("SomeSimpleAction", "Some")).Returns("http://Some/SomeSimpleAction");

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

    }
}
