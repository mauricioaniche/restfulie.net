using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Moq;
using NUnit.Framework;
using Restfulie.Server.Exceptions;
using Restfulie.Server.Marshalling;
using Restfulie.Server.Negotiation;
using Restfulie.Server.Results;
using Restfulie.Server.Tests.Fixtures;
using Restfulie.Server.Unmarshalling;

namespace Restfulie.Server.Tests
{
    [TestFixture]
    public class ActAsRestfulieTests
    {
        private Mock<IRequestInfoFinder> requestInfo;
        private ActionExecutingContext context;
        private Mock<IMarshallerFactory> marshallerFactory;
        private Mock<IUnmarshallerFactory> unmarshallerFactory;

        [SetUp]
        public void SetUp()
        {
            context = new ActionExecutingContext();
            context.ActionParameters = new Dictionary<string, object>();

            marshallerFactory = new Mock<IMarshallerFactory>();
            requestInfo = new Mock<IRequestInfoFinder>();
            unmarshallerFactory = new Mock<IUnmarshallerFactory>();
        }

        [Test]
        public void ShouldReturnNotAcceptableWhenMediaTypeIsNotSupported()
        {
            marshallerFactory.Setup(f => f.BasedOnMediaType(It.IsAny<string>())).Throws(new MediaTypeNotSupportedException());
            requestInfo.Setup(ah => ah.GetAcceptHeaderIn(context)).Returns("some-crazy-media-type");

            var filter = new ActAsRestfulie(marshallerFactory.Object, unmarshallerFactory.Object, requestInfo.Object);
            
            filter.OnActionExecuting(context);

            Assert.IsTrue(context.Result is NotAcceptable);
        }

        [Test]
        public void ShouldUnmarshallResource()
        {
            var filter = new ActAsRestfulie(marshallerFactory.Object, unmarshallerFactory.Object, requestInfo.Object)
                             {
                                 Name = "Resource",
                                 Type = typeof (SomeResource)
                             };

            var resource = new SomeResource {Amount = 123, Name = "Some name"};
            var unmarshaller = new Mock<IResourceUnmarshaller>();
            unmarshaller.Setup(u => u.ToResource("some xml", typeof (SomeResource))).Returns(resource);
            requestInfo.Setup(ah => ah.GetContentTypeIn(context)).Returns("application/xml");
            requestInfo.Setup(ah => ah.GetContent(context)).Returns("some xml");
            unmarshallerFactory.Setup(m => m.BasedOnContentType("application/xml")).Returns(unmarshaller.Object);

            filter.OnActionExecuting(context);

            Assert.AreEqual(resource, context.ActionParameters["Resource"]);
        }

        [Test]
        public void ShouldReturnUnsupportedMediaTypeWhenContentTypeIsNotSupported()
        {
            unmarshallerFactory.Setup(f => f.BasedOnContentType(It.IsAny<string>())).Throws(new ContentTypeNotSupportedException());
            requestInfo.Setup(ah => ah.GetContentTypeIn(context)).Returns("some-crazy-media-type");

            var filter = new ActAsRestfulie(marshallerFactory.Object, unmarshallerFactory.Object, requestInfo.Object)
                             {
                                 Name = "Resource",
                                 Type = typeof (SomeResource)
                             };

            filter.OnActionExecuting(context);

            Assert.IsTrue(context.Result is UnsupportedMediaType);
        }

        [Test]
        public void ShouldReturnBadRequestWhenUnmarshallingFails()
        {
            var unmarshaller = new Mock<IResourceUnmarshaller>();
            unmarshaller.Setup(u => u.ToResource(It.IsAny<string>(), It.IsAny<Type>())).Throws(
                new UnmarshallingException("message"));
            unmarshallerFactory.Setup(f => f.BasedOnContentType(It.IsAny<string>())).Returns(unmarshaller.Object);

            var filter = new ActAsRestfulie(marshallerFactory.Object, unmarshallerFactory.Object, requestInfo.Object)
            {
                Name = "Resource",
                Type = typeof(SomeResource)
            };

            filter.OnActionExecuting(context);

            Assert.IsTrue(context.Result is BadRequest);
        }
    }
}
