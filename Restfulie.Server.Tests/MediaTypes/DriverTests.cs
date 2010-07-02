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
        public void ShouldContainSerializerHypermediaInjectorAndDeserializer()
        {
            var serializer = new Mock<IResourceSerializer>();
            var hypermediaInjector = new Mock<IHypermediaInjector>();
            var deserializer = new Mock<IResourceDeserializer>();

            var driver = new Driver(serializer.Object, hypermediaInjector.Object, deserializer.Object);

            Assert.AreSame(serializer.Object, driver.Serializer);
            Assert.AreSame(hypermediaInjector.Object, driver.HypermediaInjector);
            Assert.AreSame(deserializer.Object, driver.Deserializer);
        }
    }
}
