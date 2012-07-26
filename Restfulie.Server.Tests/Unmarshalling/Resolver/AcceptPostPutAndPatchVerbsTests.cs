using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Moq;
using NUnit.Framework;
using Restfulie.Server.Tests.Fixtures;
using Restfulie.Server.Unmarshalling.Resolver;

namespace Restfulie.Server.Tests.Unmarshalling.Resolver
{
    [TestFixture]
    public class AcceptPostPutAndPatchVerbsTests
    {
        private Mock<HttpContextBase> httpContext;
        private Mock<HttpRequestBase> httpRequest;
        private RequestContext requestContext;
        private ControllerContext controllerContext;
        private ActionExecutingContext context;

        [SetUp]
        public void SetupAContext()
        {
            httpContext = new Mock<HttpContextBase>();
            httpRequest = new Mock<HttpRequestBase>();
            requestContext = new RequestContext(httpContext.Object, new RouteData());
            controllerContext = new ControllerContext(httpContext.Object, new RouteData(), new SomeController());
            httpContext.Setup(h => h.Request).Returns(httpRequest.Object);
            context = new ActionExecutingContext(controllerContext, new Mock<ActionDescriptor>().Object, new RouteValueDictionary()) { RequestContext = requestContext };
        }

        [Test]
        public void ShouldAcceptPost()
        {
            httpRequest.Setup(h => h.HttpMethod).Returns("POST");

            Assert.IsTrue(new AcceptPostPutAndPatchVerbs().IsValid(context));
        }

        [Test]
        public void ShouldAcceptPut()
        {
            httpRequest.Setup(h => h.HttpMethod).Returns("PUT");

            Assert.IsTrue(new AcceptPostPutAndPatchVerbs().IsValid(context));
        }

        [Test]
        public void ShouldAcceptPatch()
        {
            httpRequest.Setup(h => h.HttpMethod).Returns("PATCH");

            Assert.IsTrue(new AcceptPostPutAndPatchVerbs().IsValid(context));
        }

        [Test]
        public void ShouldRefuseOthers()
        {
            httpRequest.Setup(h => h.HttpMethod).Returns("GET");

            Assert.IsFalse(new AcceptPostPutAndPatchVerbs().IsValid(context));
        }
    }
}
