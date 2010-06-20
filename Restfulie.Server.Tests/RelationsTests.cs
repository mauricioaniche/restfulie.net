using System;
using System.Collections.Generic;
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
            urlGenerator.Setup(ug => ug.For("Some", "SomeSimpleAction", It.IsAny<IDictionary<string, object>>())).Returns("http://Some/SomeSimpleAction");

            var relations = new Relations(urlGenerator.Object);
            relations.Named("pay").Uses<SomeController>().SomeSimpleAction();

            var all = relations.GetAll();

            Assert.AreEqual("pay", all.First().Name);
            Assert.AreEqual("http://Some/SomeSimpleAction", all.First().Url);
        }

        [Test]
        public void ShouldDetectWrongUseOfFluentAPI()
        {
            var relations = new Relations(new Mock<IUrlGenerator>().Object);
            Assert.Throws<ArgumentException>(() =>
			relations.Uses<SomeController>().SomeSimpleAction());
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

        [Test]
        public void ShouldCreateALinkToaNonAction()
        {
            var relations = new Relations(new Mock<IUrlGenerator>().Object);

            relations.Named("to_another_website").At("some/url");

            var all = relations.GetAll();

            Assert.AreEqual(1, all.Count);
            Assert.AreEqual("to_another_website" , all.First().Name);
            Assert.AreEqual("some/url", all.First().Url);
        }
    }
}