using NUnit.Framework;
using Restfulie.Server.Results;

namespace Restfulie.Server.Tests.Results
{
    [TestFixture]
    public class NotAcceptableTests : ResultsTestBase
    {
        [SetUp]
        public void SetUp()
        {
            SetUpRequest();
        }

        [Test]
        public void ShouldReturnStatusCode406()
        {
            response.SetupSet(c => c.StatusCode = (int)StatusCodes.NotAcceptable);

            var result = new NotAcceptable();

            result.ExecuteResult(context.Object);

            response.VerifyAll();
        }
    }
}
