using Moq;
using NUnit.Framework;
using Restfulie.Server.Marshalling;
using Restfulie.Server.Results;

namespace Restfulie.Server.Tests.Results
{
    [TestFixture]
    public class SeeOtherTests : ResultsTestBase
    {
        [SetUp]
        public void SetUp()
        {
            SetUpRequest();
        }

        [Test]
        public void ShouldReturnStatusCode303()
        {
            response.SetupSet(c => c.StatusCode = (int)StatusCodes.SeeOther);

            var result = new SeeOther("new location")
            {
                Marshaller = new Mock<IResourceMarshaller>().Object
            };

            result.ExecuteResult(context.Object);

            response.VerifySet(c => c.StatusCode = (int)StatusCodes.SeeOther);
        }
    }
}
