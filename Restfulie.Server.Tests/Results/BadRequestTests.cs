using System.IO;
using System.Web;
using System.Web.Mvc;
using Moq;
using NUnit.Framework;
using Restfulie.Server.Results;

namespace Restfulie.Server.Tests.Results
{
    [TestFixture]
    public class BadRequestTests : ResultsTestBase
    {
        [SetUp]
        public void SetUp()
        {
            SetUpRequest();
        }

        [Test]
        public void ShouldReturnStatusCode400()
        {
            response.SetupSet(c => c.StatusCode = (int)StatusCodes.BadRequest);

            var result = new BadRequest();
            result.ExecuteResult(context.Object);

            response.VerifyAll();
        }

    }
}