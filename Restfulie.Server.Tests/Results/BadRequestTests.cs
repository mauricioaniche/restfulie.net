using NUnit.Framework;
using Restfulie.Server.Results;

namespace Restfulie.Server.Tests.Results
{
    [TestFixture]
    public class BadRequestTests
    {
        [Test]
        public void ShouldReturnStatusCode400()
        {
            var result = new BadRequest();

            Assert.AreEqual((int)StatusCodes.BadRequest, result.StatusCode);
        }

    }
}