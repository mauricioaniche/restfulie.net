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
            Assert.AreEqual(string.Empty, new AspNetMvcMarshaller().Build(new SomeResource()));
        }

        [Test]
        public void ShouldDoNothingWithAListOfResources()
        {
            Assert.AreEqual(string.Empty, new AspNetMvcMarshaller().Build(new[] {new SomeResource()}));
        }
    }
}
