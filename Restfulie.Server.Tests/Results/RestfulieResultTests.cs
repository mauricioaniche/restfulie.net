using System.IO;
using System.Web.Mvc;
using Moq;
using NUnit.Framework;
using Restfulie.Server.Marshalling;
using Restfulie.Server.Results;
using Restfulie.Server.Tests.Fixtures;

namespace Restfulie.Server.Tests.Results
{
    [TestFixture]
    public class RestfulieResultTests : ResultsTestBase
    {
        private SomeResource aSimpleResource;
        private Mock<IResourceMarshaller> marshaller;

        [SetUp]
        public void SetUp()
        {
            SetUpRequest();

            marshaller = new Mock<IResourceMarshaller>();
            aSimpleResource = new SomeResource { Amount = 123.45, Name = "John Doe" };
        }

        [Test]
        public void ShouldReturnResource()
        {
            marshaller.Setup(
                s => s.Build(It.IsAny<ControllerContext>(), It.Is<MarshallingInfo>(m => m.Resource == aSimpleResource)));
            var result = new SomeResult(aSimpleResource) { Marshaller = marshaller.Object };

            result.ExecuteResult(context.Object);

            marshaller.VerifyAll();
        }

        [Test]
        public void ShouldReturnResources()
        {
            var resources = new System.Collections.Generic.List<IBehaveAsResource> { aSimpleResource, aSimpleResource };

            marshaller.Setup(
                s => s.Build(It.IsAny<ControllerContext>(), It.Is<MarshallingInfo>(m => m.Resources == resources)));
            var result = new SomeResult(resources) { Marshaller = marshaller.Object };

            result.ExecuteResult(context.Object);

            marshaller.VerifyAll();
        }

        [Test]
        public void ShouldReturnMessage()
        {
            marshaller.Setup(
                s => s.Build(It.IsAny<ControllerContext>(), It.Is<MarshallingInfo>(m => m.Message == "msg")));
            var result = new SomeResult("msg") { Marshaller = marshaller.Object };

            result.ExecuteResult(context.Object);

            marshaller.VerifyAll();
        }


        [Test]
        public void ShouldSetLocation()
        {
            var result = new SomeResult { Marshaller = marshaller.Object };
            result.SetLocation("some/location");

            result.ExecuteResult(It.IsAny<ControllerContext>());

            marshaller.Verify(m => m.Build(null, It.Is<MarshallingInfo>(info => info.Location == "some/location")));
        }
    }
}
