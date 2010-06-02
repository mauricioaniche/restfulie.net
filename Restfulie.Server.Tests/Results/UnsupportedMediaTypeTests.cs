using NUnit.Framework;
using Restfulie.Server.Results;

namespace Restfulie.Server.Tests.Results
{
    [TestFixture]
    public class UnsupportedMediaTypeTests
    {
        [Test]
        public void ShouldReturnStatusCode415()
        {
            var result = new UnsupportedMediaType();

            Assert.AreEqual((int)StatusCodes.UnsupportedMediaType, result.StatusCode);
        }
    }

}
