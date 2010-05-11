using System.IO;
using System.Web;
using System.Web.Mvc;
using Moq;
using NUnit.Framework;
using Restfulie.Server.Results;

namespace Restfulie.Server.Tests.Results
{
    [TestFixture]
    public class BadRequestTests
    {
        private Mock<HttpResponseBase> response;
        private Mock<HttpContextBase> http;
        private Mock<ControllerContext> context;
        private MemoryStream stream;

        [SetUp]
        public void SetUp()
        {
            response = new Mock<HttpResponseBase>();
            http = new Mock<HttpContextBase>();
            context = new Mock<ControllerContext>();

            http.Setup(h => h.Response).Returns(response.Object);
            context.Setup(c => c.HttpContext).Returns(http.Object);

            stream = new MemoryStream();
        }

        [Test]
        public void ShouldReturnStatusCode400()
        {
            response.SetupSet(c => c.StatusCode = (int)StatusCodes.BadRequest);

            var result = new BadRequest();
            result.ExecuteResult(context.Object);

            response.VerifyAll();
        }

        [Test]
        public void ShouldReturnAMessage()
        {
            response.Setup(p => p.Output).Returns(new StreamWriter(stream));
            var result = new BadRequest("error message");

            result.ExecuteResult(context.Object);

            stream.Seek(0, SeekOrigin.Begin);
            var serializedResource = new StreamReader(stream).ReadToEnd();

            Assert.That(serializedResource.Contains("error message"));
        }
    }
}