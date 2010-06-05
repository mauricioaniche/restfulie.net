using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Moq;
using NUnit.Framework;
using Restfulie.Server.Tests.Fixtures;
using Restfulie.Server.Unmarshalling;
using System.Linq;

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

        private IList<ParameterDescriptor> parameterList;

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
   
            parameterList = new List<ParameterDescriptor>();
        }

        [Test]
        public void ItShouldUnmarshallOnlyWhenVerbIsAPostOrPut()
        {
            httpRequest.Setup(h => h.HttpMethod).Returns("GET");

            var resolver = new UnmarshallerResolver();
            resolver.DetectIn(context);

            Assert.IsFalse(resolver.HasResource);
        }

        [Test]
        public void ShouldDetectIfActionExpectsAResource()
        {
            httpRequest.Setup(h => h.HttpMethod).Returns("POST");

            CreateParameter("parameter", typeof(SomeResource));
            actionDescriptor.Setup(a => a.GetParameters()).Returns(parameterList.ToArray());

            var resolver = new UnmarshallerResolver();
            resolver.DetectIn(context);

            Assert.IsTrue(resolver.HasResource);
        }

        [Test]
        public void ShouldFindTheResourceParameter()
        {
            httpRequest.Setup(h => h.HttpMethod).Returns("POST");

            CreateParameter("resource", typeof(SomeResource));
            CreateParameter("nonResource", typeof(int));
            actionDescriptor.Setup(a => a.GetParameters()).Returns(parameterList.ToArray());

            var resolver = new UnmarshallerResolver();
            resolver.DetectIn(context);

            Assert.AreEqual(typeof(SomeResource), resolver.ParameterType);
            Assert.AreEqual("resource", resolver.ParameterName);
        }

        [Test]
        public void ShouldDetectIfItIsAListOfResources()
        {
            httpRequest.Setup(h => h.HttpMethod).Returns("POST");

            CreateParameter("nonResource", typeof (int));
            CreateParameter("resource", new[] { new SomeResource()}.GetType());

            actionDescriptor.Setup(a => a.GetParameters()).Returns(parameterList.ToArray());

            var resolver = new UnmarshallerResolver();
            resolver.DetectIn(context);

            Assert.IsTrue(resolver.HasResource);
            Assert.AreEqual(typeof(SomeResource[]), resolver.ParameterType);
            Assert.AreEqual("resource", resolver.ParameterName);
        }


        private void CreateParameter(string name, Type type)
        {
            var parameter = new Mock<ParameterDescriptor>();
            parameter.Setup(p => p.ParameterType).Returns(type);
            parameter.Setup(p => p.ParameterName).Returns(name);
            parameterList.Add(parameter.Object);
        }
    }
}
