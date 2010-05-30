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
            Assert.AreEqual("Some", all.First().Controller);
            Assert.AreEqual("SomeSimpleAction", all.First().Action);
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
        public void ShouldCaptureValuesPassed()
        {
            var relations = new Relations(new Mock<IUrlGenerator>().Object);
            relations.Named("pay").Uses<SomeController>().ActionWithParameter(123, 456);

            var all = relations.GetAll();

            Assert.AreEqual(2, all.First().Values.Count);
            Assert.AreEqual("123", Convert.ToString(all.First().Values["id"]));
            Assert.AreEqual("456", Convert.ToString(all.First().Values["qty"]));
        }

        [Test]
        public void ShouldIgnoreValuesPassedAsNull()
        {
            var relations = new Relations(new Mock<IUrlGenerator>().Object);
            relations.Named("pay").Uses<SomeController>().ActionWithResource(null);

            var all = relations.GetAll();

            Assert.AreEqual(0, all.First().Values.Count);
        }
    }
}