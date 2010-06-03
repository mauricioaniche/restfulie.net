using System.Net;
using NUnit.Framework;
using Restfulie.Server.Results;

namespace Restfulie.Server.Tests.Results
{
    [TestFixture]
    public class InternalServerErrorTests
    {
        [Test]
        public void ShouldReturnStatusCode500()
        {
            var result = new InternalServerError();

            Assert.AreEqual((int)HttpStatusCode.InternalServerError, result.StatusCode);
        }
    }
}
