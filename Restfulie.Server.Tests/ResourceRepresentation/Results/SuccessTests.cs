using System.IO;
using System.Web;
using System.Web.Mvc;
using Moq;
using NUnit.Framework;
using Restfulie.Server.ResourceRepresentation;
using Restfulie.Server.Results;

namespace Restfulie.Server.Tests.ResourceRepresentation.Results
{
    [TestFixture]
    public class SuccessTests
    {
        private Mock<HttpResponseBase> response;
        private Mock<HttpContextBase> http;
        private Mock<ControllerContext> context;
        private MemoryStream stream;
        private Mock<IRepresentationBuilder> builder;
        private SomeResource aSimpleResource;

        [SetUp]
        public void SetUp()
        {
            response = new Mock<HttpResponseBase>();
            http = new Mock<HttpContextBase>();
            context = new Mock<ControllerContext>();

            http.Setup(h => h.Response).Returns(response.Object);
            context.Setup(c => c.HttpContext).Returns(http.Object);
            
            stream = new MemoryStream();
            builder = new Mock<IRepresentationBuilder>();

            aSimpleResource = new SomeResource { Amount = 123.45, Name = "John Doe" };
        }

        [Test]
        public void ShouldReturnStatusCode200()
        {
            response.SetupSet(c => c.StatusCode = (int)StatusCodes.Success);

            var result = new Success();
            result.ExecuteResult(context.Object);

            response.VerifyAll();
        }

        [Test]
        public void ShouldReturnResource()
        {
            response.Setup(p => p.Output).Returns(new StreamWriter(stream));

            builder.Setup(s => s.Build(aSimpleResource)).Returns(
                "<SomeResource><Name>John Doe</name><amount>123.45</amount></SomeResource>");
            var result = new Success(aSimpleResource, builder.Object);

            result.ExecuteResult(context.Object);

            stream.Seek(0, SeekOrigin.Begin);
            var serializedResource = new StreamReader(stream).ReadToEnd();

            Assert.That(serializedResource.Contains("John Doe"));
            Assert.That(serializedResource.Contains("123.45"));
            builder.VerifyAll();
        }
    }
}