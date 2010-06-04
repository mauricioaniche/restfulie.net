using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Moq;
using NUnit.Framework;
using Restfulie.Server.Marshalling;
using Restfulie.Server.MediaTypes;
using Restfulie.Server.Negotiation;
using Restfulie.Server.Results;
using Restfulie.Server.Results.Decorators.Holders;
using Restfulie.Server.Tests.Fixtures;
using Restfulie.Server.Unmarshalling;

namespace Restfulie.Server.Tests
{
    [TestFixture]
    public class ActAsRestfulieTests
    {
        private Mock<IRequestInfoFinder> requestInfo;
        private Mock<IResourceUnmarshaller> unmarshaller;
        private Mock<IMediaType> mediaType;
        private Mock<IResourceMarshaller> marshaller;
        private Mock<IAcceptHeaderToMediaType> acceptHeader;
        private Mock<IContentTypeToMediaType> contentType;
        private Mock<IResultDecoratorHolderFactory> resultHolderFactory;
        private Mock<IResultDecoratorHolder> resultHolder;
        private Mock<IUnmarshallerResolver> resolver;

        private ActionExecutingContext actionExecutingContext;
        private ResultExecutingContext resultExecutingContext;

        [SetUp]
        public void SetUp()
        {
            actionExecutingContext = new ActionExecutingContext
                          {
                              ActionParameters = new Dictionary<string, object>()
                          };

            resultExecutingContext = new ResultExecutingContext();

            requestInfo = new Mock<IRequestInfoFinder>();
            acceptHeader = new Mock<IAcceptHeaderToMediaType>();
            contentType = new Mock<IContentTypeToMediaType>();

            unmarshaller = new Mock<IResourceUnmarshaller>();
            marshaller = new Mock<IResourceMarshaller>();

            resultHolderFactory = new Mock<IResultDecoratorHolderFactory>();
            resultHolder = new Mock<IResultDecoratorHolder>();

            resolver = new Mock<IUnmarshallerResolver>();

            mediaType = new Mock<IMediaType>();
            mediaType.SetupGet(m => m.Unmarshaller).Returns(unmarshaller.Object);
            mediaType.SetupGet(m => m.Marshaller).Returns(marshaller.Object);
        }

        [Test]
        public void ShouldSetMediaTypeAndResultHolderToResult()
        {
            resultExecutingContext.Result = new SomeResult();
            var filter = new ActAsRestfulie(acceptHeader.Object, contentType.Object, requestInfo.Object, resultHolderFactory.Object, resolver.Object);

            acceptHeader.Setup(ah => ah.GetMediaType(It.IsAny<string>())).Returns(mediaType.Object);
            resultHolderFactory.Setup(factory => factory.BasedOn(mediaType.Object)).Returns(resultHolder.Object);

            filter.OnActionExecuting(actionExecutingContext);
            filter.OnResultExecuting(resultExecutingContext);


            Assert.AreEqual(mediaType.Object, ((RestfulieResult)resultExecutingContext.Result).MediaType);
            Assert.AreEqual(resultHolder.Object, ((RestfulieResult)resultExecutingContext.Result).ResultHolder);
        }

        [Test]
        public void ShouldReturnNotAcceptableWhenMediaTypeIsNotSupported()
        {
            acceptHeader.Setup(f => f.GetMediaType(It.IsAny<string>())).Throws(new AcceptHeaderNotSupportedException());
            requestInfo.Setup(ah => ah.GetAcceptHeaderIn(actionExecutingContext)).Returns("some-crazy-media-type");

            var filter = new ActAsRestfulie(acceptHeader.Object, contentType.Object, requestInfo.Object, resultHolderFactory.Object, resolver.Object);

            filter.OnActionExecuting(actionExecutingContext);

            Assert.IsTrue(actionExecutingContext.Result is NotAcceptable);
        }

        [Test]
        public void ShouldUnmarshallResource()
        {
            var filter = new ActAsRestfulie(acceptHeader.Object, contentType.Object, requestInfo.Object,
                                            resultHolderFactory.Object, resolver.Object);

            var resource = new SomeResource { Amount = 123, Name = "Some name" };

            resolver.SetupGet(r => r.HasResource).Returns(true);
            resolver.SetupGet(r => r.ParameterName).Returns("Resource");
            resolver.SetupGet(r => r.ParameterType).Returns(typeof (SomeResource));

            unmarshaller.Setup(u => u.ToResource(It.IsAny<string>(), typeof(SomeResource))).Returns(resource);
            contentType.Setup(m => m.GetMediaType(It.IsAny<string>())).Returns(mediaType.Object);

            filter.OnActionExecuting(actionExecutingContext);

            Assert.AreEqual(resource, actionExecutingContext.ActionParameters["Resource"]);
        }

        [Test]
        public void ShouldUnmarshallListOfResources()
        {
            var filter = new ActAsRestfulie(acceptHeader.Object, contentType.Object, requestInfo.Object,
                                            resultHolderFactory.Object, resolver.Object);

            var resources = new[] {new SomeResource { Amount = 123, Name = "Some name" } };

            resolver.SetupGet(r => r.HasListOfResources).Returns(true);
            resolver.SetupGet(r => r.ParameterName).Returns("Resource");
            resolver.SetupGet(r => r.ParameterType).Returns(typeof(SomeResource));

            unmarshaller.Setup(u => u.ToListOfResources(It.IsAny<string>(), typeof(SomeResource))).Returns(resources);
            contentType.Setup(m => m.GetMediaType(It.IsAny<string>())).Returns(mediaType.Object);

            filter.OnActionExecuting(actionExecutingContext);

            Assert.AreEqual(resources, actionExecutingContext.ActionParameters["Resource"]);            
        }

        [Test]
        public void ShouldReturnUnsupportedMediaTypeWhenContentTypeIsNotSupported()
        {
            contentType.Setup(f => f.GetMediaType(It.IsAny<string>())).Throws(new ContentTypeNotSupportedException());
            requestInfo.Setup(ah => ah.GetContentTypeIn(actionExecutingContext)).Returns("some-crazy-media-type");

            var filter = new ActAsRestfulie(acceptHeader.Object, contentType.Object, requestInfo.Object,
                                            resultHolderFactory.Object, resolver.Object);

            filter.OnActionExecuting(actionExecutingContext);

            Assert.IsTrue(actionExecutingContext.Result is UnsupportedMediaType);
        }

        [Test]
        public void ShouldReturnBadRequestWhenUnmarshallingFails()
        {
            resolver.SetupGet(r => r.HasResource).Returns(true);

            unmarshaller.Setup(u => u.ToResource(It.IsAny<string>(), It.IsAny<Type>())).Throws(
                new UnmarshallingException("message"));

            contentType.Setup(f => f.GetMediaType(It.IsAny<string>())).Returns(mediaType.Object);

            var filter = new ActAsRestfulie(acceptHeader.Object, contentType.Object, requestInfo.Object,
                                            resultHolderFactory.Object, resolver.Object);
            filter.OnActionExecuting(actionExecutingContext);

            Assert.IsTrue(actionExecutingContext.Result is BadRequest);
        }
    }
}
