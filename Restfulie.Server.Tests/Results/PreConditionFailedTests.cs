using NUnit.Framework;
using Restfulie.Server.Results;

namespace Restfulie.Server.Tests.Results
{
    [TestFixture]
    public class PreConditionFailedTests
    {

        [Test]
        public void ShouldReturnStatusCode412()
        {
            var result = new PreConditionFailed();
         
            Assert.AreEqual((int)StatusCodes.PreConditionFailed, result.StatusCode);
        }
    }
}
