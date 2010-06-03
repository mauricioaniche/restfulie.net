using System.Net;
using NUnit.Framework;
using Restfulie.Server.Results;

namespace Restfulie.Server.Tests.Results
{
    [TestFixture]
    public class SeeOtherTests 
    {

        [Test]
        public void ShouldReturnStatusCode303()
        {
            var result = new SeeOther("new location");

            Assert.AreEqual((int)HttpStatusCode.SeeOther, result.StatusCode);
        }
    }
}
