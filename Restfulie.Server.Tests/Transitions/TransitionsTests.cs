using System.Linq;
using NUnit.Framework;

namespace Restfulie.Server.Tests.Transitions
{
    [TestFixture]
    public class TransitionsTests
    {
        [Test]
        public void ShouldTransitToAControllerAction()
        {
            var transit = new Server.Transitions();
            transit.Named("pay").Uses<SomeController>().SomeSimpleAction();

            Assert.AreEqual("pay", transit.All.First().Name);
        }

    }
}
