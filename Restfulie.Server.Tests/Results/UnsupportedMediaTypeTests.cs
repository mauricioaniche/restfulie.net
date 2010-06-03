using System.Net;
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

            Assert.AreEqual((int)HttpStatusCode.UnsupportedMediaType, result.StatusCode);
        }
    }

}
