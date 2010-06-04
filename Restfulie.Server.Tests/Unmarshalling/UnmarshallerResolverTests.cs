using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Moq;
using NUnit.Framework;
using Restfulie.Server.Tests.Fixtures;
using Restfulie.Server.Unmarshalling;

namespace Restfulie.Server.Tests.Unmarshalling
{
    [TestFixture]
    public class UnmarshallerResolverTests
    {
        private Mock<HttpContextBase> httpContext;
        private Mock<HttpRequestBase> httpRequest;
        private RequestContext requestContext;
        private SomeController someController;
        private ControllerContext controllerContext;
        private Mock<ActionDescriptor> actionDescriptor;
        private ActionExecutingContext context;

        [SetUp]
        public void CrazySetup()
        {
            httpContext = new Mock<HttpContextBase>();
            httpRequest = new Mock<HttpRequestBase>();
            requestContext = new RequestContext(httpContext.Object, new RouteData());

            someController = new SomeController();
            controllerContext = new ControllerContext(httpContext.Object, new RouteData(), someController);
            actionDescriptor = new Mock<ActionDescriptor>();

            httpContext.Setup(h => h.Request).Returns(httpRequest.Object);
            context = new ActionExecutingContext(controllerContext, actionDescriptor.Object, new RouteValueDictionary()) { RequestContext = requestContext };
   
        }

        [Test]
        public void ItShouldUnmarshallOnlyWhenVerbIsAPostOrPut()
        {
            httpRequest.Setup(h => h.HttpMethod).Returns("GET");

            var resolver = new UnmarshallerResolver();
            resolver.DetectIn(context);

            Assert.IsFalse(resolver.HasResource);
            Assert.IsFalse(resolver.HasListOfResources);
        }

        [Test]
        public void ShouldDetectIfActionExpectsAResource()
        {
            httpRequest.Setup(h => h.HttpMethod).Returns("POST");

            var resourceParameter = new Mock<ParameterDescriptor>();
            resourceParameter.Setup(p => p.ParameterType).Returns(typeof(IBehaveAsResource));
            var parameterList = new[] { resourceParameter.Object };
            actionDescriptor.Setup(a => a.GetParameters()).Returns(parameterList);

            var resolver = new UnmarshallerResolver();
            resolver.DetectIn(context);

            Assert.IsTrue(resolver.HasResource);
            Assert.IsFalse(resolver.HasListOfResources);
        }

        [Test]
        public void ShouldFindTheResourceParameter()
        {
            httpRequest.Setup(h => h.HttpMethod).Returns("POST");

            var nonResourceParameter = new Mock<ParameterDescriptor>();
            var resourceParameter = new Mock<ParameterDescriptor>();

            resourceParameter.Setup(p => p.ParameterType).Returns(typeof(SomeResource));
            resourceParameter.Setup(p => p.ParameterName).Returns("resource");
            nonResourceParameter.Setup(p => p.ParameterType).Returns(typeof (int));

            var parameterList = new[] { nonResourceParameter.Object, resourceParameter.Object };
            actionDescriptor.Setup(a => a.GetParameters()).Returns(parameterList);

            var resolver = new UnmarshallerResolver();
            resolver.DetectIn(context);

            Assert.AreEqual(typeof(SomeResource), resolver.Type);
            Assert.AreEqual("resource", resolver.ParameterName);
        }

        [Test]
        public void ShouldDetectIfItIsAListOfResources()
        {
            httpRequest.Setup(h => h.HttpMethod).Returns("POST");

            var nonResourceParameter = new Mock<ParameterDescriptor>();
            var resourceParameter = new Mock<ParameterDescriptor>();

            resourceParameter.Setup(p => p.ParameterType).Returns(new[] { new SomeResource()}.GetType());
            resourceParameter.Setup(p => p.ParameterName).Returns("resource");
            nonResourceParameter.Setup(p => p.ParameterType).Returns(typeof(int));

            var parameterList = new[] { nonResourceParameter.Object, resourceParameter.Object };
            actionDescriptor.Setup(a => a.GetParameters()).Returns(parameterList);

            var resolver = new UnmarshallerResolver();
            resolver.DetectIn(context);

            Assert.IsTrue(resolver.HasListOfResources);
            Assert.IsFalse(resolver.HasResource);
            Assert.AreEqual(typeof(SomeResource), resolver.Type);
            Assert.AreEqual("resource", resolver.ParameterName);
        }
    }
}
