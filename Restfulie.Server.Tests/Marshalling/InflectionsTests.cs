using NUnit.Framework;
using Restfulie.Server.Marshalling;

namespace Restfulie.Server.Tests.Marshalling
{
    [TestFixture]
    public class InflectionsTests
    {
        [Test]
        public void ShouldInflectAName()
        {
            Assert.AreEqual("SomeResources", new DefaultInflections().Inflect("SomeResource"));
        }
    }
}
