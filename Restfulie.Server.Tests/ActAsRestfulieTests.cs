using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Moq;
using NUnit.Framework;
using Restfulie.Server.Marshalling;
using Restfulie.Server.MediaTypes;
using Restfulie.Server.Negotiation;
using Restfulie.Server.Results;
using Restfulie.Server.Results.Chooser;
using Restfulie.Server.Tests.Fixtures;
using Restfulie.Server.Unmarshalling;
using Restfulie.Server.Unmarshalling.Resolver;

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
        private Mock<IResultChooser> chooser;
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

            chooser = new Mock<IResultChooser>();

            resolver = new Mock<IUnmarshallerResolver>();

            mediaType = new Mock<IMediaType>();
            mediaType.SetupGet(m => m.Unmarshaller).Returns(unmarshaller.Object);
            mediaType.SetupGet(m => m.Marshaller).Returns(marshaller.Object);
        }

        [Test]
        public void ShouldReturnNotAcceptableWhenMediaTypeIsNotSupported()
        {
            acceptHeader.Setup(f => f.GetMediaType(It.IsAny<string>())).Throws(new AcceptHeaderNotSupportedException());
            requestInfo.Setup(ah => ah.GetAcceptHeaderIn(actionExecutingContext)).Returns("some-crazy-media-type");

            var filter = new ActAsRestfulie(acceptHeader.Object, contentType.Object, requestInfo.Object, chooser.Object, resolver.Object);

            filter.OnActionExecuting(actionExecutingContext);

            Assert.IsTrue(actionExecutingContext.Result is NotAcceptable);
        }

        [Test]
        public void ShouldUnmarshallResource()
        {
            var filter = new ActAsRestfulie(acceptHeader.Object, contentType.Object, requestInfo.Object,
                                            chooser.Object, resolver.Object);

            var resource = new SomeResource { Amount = 123, Name = "Some name" };

            resolver.SetupGet(r => r.HasResource).Returns(true);
            resolver.SetupGet(r => r.ParameterName).Returns("Resource");
            resolver.SetupGet(r => r.ParameterType).Returns(typeof (SomeResource));

            unmarshaller.Setup(u => u.Build(It.IsAny<string>(), typeof(SomeResource))).Returns(resource);
            contentType.Setup(m => m.GetMediaType(It.IsAny<string>())).Returns(mediaType.Object);

            filter.OnActionExecuting(actionExecutingContext);

            Assert.AreEqual(resource, actionExecutingContext.ActionParameters["Resource"]);
        }

        [Test]
        public void ShouldUnmarshallListOfResources()
        {
            var filter = new ActAsRestfulie(acceptHeader.Object, contentType.Object, requestInfo.Object,
                                            chooser.Object, resolver.Object);

            var resources = new[] {new SomeResource { Amount = 123, Name = "Some name" } };

            resolver.SetupGet(r => r.HasResource).Returns(true);
            resolver.SetupGet(r => r.ParameterName).Returns("Resource");
            resolver.SetupGet(r => r.ParameterType).Returns(typeof(SomeResource));

            unmarshaller.Setup(u => u.Build(It.IsAny<string>(), typeof(SomeResource))).Returns(resources);
            contentType.Setup(m => m.GetMediaType(It.IsAny<string>())).Returns(mediaType.Object);

            filter.OnActionExecuting(actionExecutingContext);

            Assert.AreEqual(resources, actionExecutingContext.ActionParameters["Resource"]);            
        }

        [Test]
        public void ShouldReturnUnsupportedMediaTypeWhenContentTypeIsNotSupported()
        {
            resolver.Setup(r => r.HasResource).Returns(true);
            contentType.Setup(f => f.GetMediaType(It.IsAny<string>())).Throws(new ContentTypeNotSupportedException());
            requestInfo.Setup(ah => ah.GetContentTypeIn(actionExecutingContext)).Returns("some-crazy-media-type");

            var filter = new ActAsRestfulie(acceptHeader.Object, contentType.Object, requestInfo.Object,
                                            chooser.Object, resolver.Object);

            filter.OnActionExecuting(actionExecutingContext);

            Assert.IsTrue(actionExecutingContext.Result is UnsupportedMediaType);
        }

        [Test]
        public void ShouldReturnBadRequestWhenUnmarshallingFails()
        {
            resolver.SetupGet(r => r.HasResource).Returns(true);

            unmarshaller.Setup(u => u.Build(It.IsAny<string>(), It.IsAny<Type>())).Throws(
                new UnmarshallingException("message"));

            contentType.Setup(f => f.GetMediaType(It.IsAny<string>())).Returns(mediaType.Object);

            var filter = new ActAsRestfulie(acceptHeader.Object, contentType.Object, requestInfo.Object,
                                            chooser.Object, resolver.Object);
            filter.OnActionExecuting(actionExecutingContext);

            Assert.IsTrue(actionExecutingContext.Result is BadRequest);
        }

        [Test]
        public void ShouldIgnoreUnmarshallingIfThereIsNothingToBeUnmarshalled()
        {
            resolver.SetupGet(r => r.HasResource).Returns(false);

            contentType.Setup(f => f.GetMediaType(It.IsAny<string>())).Throws(new ContentTypeNotSupportedException());

            var filter = new ActAsRestfulie(acceptHeader.Object, contentType.Object, requestInfo.Object,
                                chooser.Object, resolver.Object);
            filter.OnActionExecuting(actionExecutingContext);

            contentType.Verify(c => c.GetMediaType(It.IsAny<string>()), Times.Never());
            unmarshaller.Verify(u => u.Build(It.IsAny<string>(), It.IsAny<Type>()), Times.Never());
            unmarshaller.Verify(u => u.Build(It.IsAny<string>(), It.IsAny<Type>()), Times.Never());
            
        }

        [Test]
        public void ShouldNotReplaceResourceIfUnmarshallerReturnsNull()
        {
            var filter = new ActAsRestfulie(acceptHeader.Object, contentType.Object, requestInfo.Object,
                                            chooser.Object, resolver.Object);

            actionExecutingContext.ActionParameters["Resource"] = "some old data here";

            resolver.SetupGet(r => r.HasResource).Returns(true);
            resolver.SetupGet(r => r.ParameterName).Returns("Resource");
            resolver.SetupGet(r => r.ParameterType).Returns(typeof(SomeResource));

            unmarshaller.Setup(u => u.Build(It.IsAny<string>(), typeof(SomeResource))).Returns(null as IBehaveAsResource);
            contentType.Setup(m => m.GetMediaType(It.IsAny<string>())).Returns(mediaType.Object);

            filter.OnActionExecuting(actionExecutingContext);

            Assert.AreEqual("some old data here", actionExecutingContext.ActionParameters["Resource"]);
        }
    }
}
