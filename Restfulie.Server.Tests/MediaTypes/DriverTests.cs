using Moq;
using NUnit.Framework;
using Restfulie.Server.Marshalling.Serializers;
using Restfulie.Server.MediaTypes;
using Restfulie.Server.Unmarshalling.Deserializers;

namespace Restfulie.Server.Tests.MediaTypes
{
    [TestFixture]
    public class DriverTests
    {
        [Test]
        public void ShouldContainSerializerHypermediaInserterAndDeserializer()
        {
            var serializer = new Mock<IResourceSerializer>();
            var hypermediaInserter = new Mock<IHypermediaInserter>();
            var deserializer = new Mock<IResourceDeserializer>();

            var driver = new Driver(serializer.Object, hypermediaInserter.Object, deserializer.Object);

            Assert.AreSame(serializer.Object, driver.Serializer);
            Assert.AreSame(hypermediaInserter.Object, driver.HypermediaInserter);
            Assert.AreSame(deserializer.Object, driver.Deserializer);
        }
    }
}
