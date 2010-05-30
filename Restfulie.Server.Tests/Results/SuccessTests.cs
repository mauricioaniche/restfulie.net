using System.IO;
using Moq;
using NUnit.Framework;
using Restfulie.Server.Marshalling;
using Restfulie.Server.Results;

namespace Restfulie.Server.Tests.Results
{
    [TestFixture]
    public class SuccessTests : ResultsTestBase
    {
        [SetUp]
        public void SetUp()
        {
            SetUpRequest();
        }

        [Test]
        public void ShouldReturnStatusCode200()
        {
            response.SetupSet(c => c.StatusCode = (int)StatusCodes.Success);

            var result = new Success
                             {
                                 Marshaller = new Mock<IResourceMarshaller>().Object
                             };

            result.ExecuteResult(context.Object);

            response.VerifySet(c => c.StatusCode = (int)StatusCodes.Success);
        }
    }
}