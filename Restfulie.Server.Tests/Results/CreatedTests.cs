using NUnit.Framework;
using Restfulie.Server.Results;

namespace Restfulie.Server.Tests.Results
{
    [TestFixture]
    public class CreatedTests
    {

        [Test]
        public void ShouldReturnStatusCode201()
        {
            var result = new Created();

            Assert.AreEqual((int)StatusCodes.Created, result.StatusCode);
        }

    }
}
