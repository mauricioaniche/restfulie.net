using System.IO;
using System.Web;
using System.Web.Mvc;
using Moq;
using NUnit.Framework;
using Restfulie.Server.Results.ContextDecorators;

namespace Restfulie.Server.Tests.Results.ContextDecorators
{
    [TestFixture]
    public class ContentDecoratorTests
    {
        private Mock<ControllerContext> context;
        private Mock<HttpContextBase> httpContext;
        private Mock<HttpResponseBase> httpResponseBase;
        private Mock<TextWriter> output;

        [SetUp]
        public void SetUp()
        {
            output = new Mock<TextWriter>();
            httpResponseBase = new Mock<HttpResponseBase>();
            httpContext = new Mock<HttpContextBase>();
            context = new Mock<ControllerContext>();

            context.Setup(c => c.HttpContext).Returns(httpContext.Object);
            httpContext.Setup(h => h.Response).Returns(httpResponseBase.Object);
            httpResponseBase.Setup(h => h.Output).Returns(output.Object);
        }

        [Test]
        public void ShouldSetStatusCode()
        {
            new ContentDecorator("some content here").Execute(context.Object);

            output.Verify(o => o.Write("some content here"));
            output.Verify(o => o.Flush());
        }

        [Test]
        public void ShouldCallNextDecorator()
        {
            var nextDecorator = new Mock<ContextDecorator>();

            new ContentDecorator("some content here", nextDecorator.Object).Execute(context.Object);

            nextDecorator.Verify(nd => nd.Execute(context.Object));
        }
    }
}
