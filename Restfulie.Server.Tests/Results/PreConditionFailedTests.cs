using System.Net;
using NUnit.Framework;
using Restfulie.Server.Results;

namespace Restfulie.Server.Tests.Results
{
    [TestFixture]
    public class PreconditionFailedTests
    {

        [Test]
        public void ShouldReturnStatusCode412()
        {
            var result = new PreconditionFailed();
         
            Assert.AreEqual((int)HttpStatusCode.PreconditionFailed, result.StatusCode);
        }
    }
}
