using NUnit.Framework;
using Restfulie.Server.Results;

namespace Restfulie.Server.Tests.Results
{
    [TestFixture]
    public class SuccessTests 
    {
        [Test]
        public void ShouldReturnStatusCode200()
        {
            var result = new Success();

            Assert.AreEqual((int)StatusCodes.Success, result.StatusCode);
        }
    }
}