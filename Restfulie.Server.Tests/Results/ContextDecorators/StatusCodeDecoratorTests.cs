using System.Web;
using System.Web.Mvc;
using Moq;
using NUnit.Framework;
using Restfulie.Server.Results.ContextDecorators;

namespace Restfulie.Server.Tests.Results.ContextDecorators
{
    [TestFixture]
    public class StatusCodeDecoratorTests
    {
        private Mock<ControllerContext> context;
        private Mock<HttpContextBase> httpContext;
        private Mock<HttpResponseBase> httpResponseBase;

        [SetUp]
        public void SetUp()
        {
            httpResponseBase = new Mock<HttpResponseBase>();
            httpContext = new Mock<HttpContextBase>();
            context = new Mock<ControllerContext>();

            context.Setup(c => c.HttpContext).Returns(httpContext.Object);
            httpContext.Setup(h => h.Response).Returns(httpResponseBase.Object);
        }

        [Test]
        public void ShouldSetStatusCode()
        {
            new StatusCodeDecorator(123).Execute(context.Object);

            httpResponseBase.VerifySet(h => h.StatusCode = 123);
        }

        [Test]
        public void ShouldCallNextDecorator()
        {
            var nextDecorator = new Mock<ContextDecorator>();

            new StatusCodeDecorator(123, nextDecorator.Object).Execute(context.Object);

            nextDecorator.Verify(nd => nd.Execute(context.Object));
        }
    }
}
