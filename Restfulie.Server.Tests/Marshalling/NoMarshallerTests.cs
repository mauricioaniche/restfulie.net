using NUnit.Framework;
using Restfulie.Server.Marshalling;
using Restfulie.Server.Tests.Fixtures;

namespace Restfulie.Server.Tests.Marshalling
{
    [TestFixture]
    public class NoMarshallerTests
    {
        [Test]
        public void ShouldDoNothingWithAResource()
        {
            Assert.AreEqual(string.Empty, new NoMarshaller().Build(new SomeResource()));
        }

        [Test]
        public void ShouldDoNothingWithAListOfResources()
        {
            Assert.AreEqual(string.Empty, new NoMarshaller().Build(new[] {new SomeResource()}));
        }
    }
}
