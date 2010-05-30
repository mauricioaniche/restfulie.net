using Moq;
using NUnit.Framework;
using Restfulie.Server.Marshalling;
using Restfulie.Server.Results;
using Restfulie.Server.Tests.Fixtures;

namespace Restfulie.Server.Tests.Results
{
    [TestFixture]
    public class CreatedTests : ResultsTestBase
    {
        [SetUp]
        public void SetUp()
        {
            SetUpRequest();
        }

        [Test]
        public void ShouldReturnStatusCode201()
        {
            response.SetupSet(c => c.StatusCode = (int)StatusCodes.Created);

            var result = new Created
            {
                Marshaller = new Mock<IResourceMarshaller>().Object
            };

            result.ExecuteResult(context.Object);

            response.VerifySet(c => c.StatusCode = (int)StatusCodes.Created);
        }

        [Test]
        public void ShouldSetLocation()
        {
            response.SetupSet(r => r.RedirectLocation = "some location");

            var result = new Created(new SomeResource(), "some location")
            {
                Marshaller = new Mock<IResourceMarshaller>().Object
            };

            result.ExecuteResult(context.Object);

            response.VerifySet(r => r.RedirectLocation = "some location");
        }
    }
}
