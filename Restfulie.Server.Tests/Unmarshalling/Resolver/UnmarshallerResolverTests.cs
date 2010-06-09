using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Moq;
using NUnit.Framework;
using Restfulie.Server.Tests.Fixtures;
using System.Linq;
using Restfulie.Server.Unmarshalling.Resolver;

namespace Restfulie.Server.Tests.Unmarshalling.Resolver
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
        public void ItShouldUnmarshallOnlyWhenVerbIsAPostOrPutOrPatch()
        {
            httpRequest.Setup(h => h.HttpMethod).Returns("GET");

            var resolver = new UnmarshallerResolver();
            resolver.DetectIn(context);

            Assert.IsFalse(resolver.HasResource);
        }

        [Test]
        public void ShouldUnmarshallTheFirstParameter()
        {
            httpRequest.Setup(h => h.HttpMethod).Returns("POST");

            CreateParameter("parameter", typeof(SomeResource));
            actionDescriptor.Setup(a => a.GetParameters()).Returns(parameterList.ToArray());

            var resolver = new UnmarshallerResolver();
            resolver.DetectIn(context);

            Assert.IsTrue(resolver.HasResource);
            Assert.AreEqual("parameter", resolver.ParameterName);
            Assert.AreEqual(typeof(SomeResource), resolver.ParameterType);
        }

        [Test]
        public void ShouldNotResolveActionWithoutParameter()
        {
            httpRequest.Setup(h => h.HttpMethod).Returns("POST");

            actionDescriptor.Setup(a => a.GetParameters()).Returns(parameterList.ToArray());

            var resolver = new UnmarshallerResolver();
            resolver.DetectIn(context);

            Assert.IsFalse(resolver.HasResource);
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