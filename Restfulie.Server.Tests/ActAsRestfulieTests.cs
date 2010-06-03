using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Moq;
using NUnit.Framework;
using Restfulie.Server.Marshalling;
using Restfulie.Server.MediaTypes;
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

        private Mock<IResourceUnmarshaller> unmarshaller;
        private Mock<IMediaType> mediaType;
        private Mock<IResourceMarshaller> marshaller;
        private Mock<IAcceptHeaderToMediaType> acceptHeader;
        private Mock<IContentTypeToMediaType> contentType;

        [SetUp]
        public void SetUp()
        {
            context = new ActionExecutingContext
                          {
                              ActionParameters = new Dictionary<string, object>()
                          };

            requestInfo = new Mock<IRequestInfoFinder>();
            acceptHeader = new Mock<IAcceptHeaderToMediaType>();
            contentType = new Mock<IContentTypeToMediaType>();

            unmarshaller = new Mock<IResourceUnmarshaller>();
            marshaller = new Mock<IResourceMarshaller>();

            mediaType = new Mock<IMediaType>();
            mediaType.SetupGet(m => m.Unmarshaller).Returns(unmarshaller.Object);
            mediaType.SetupGet(m => m.Marshaller).Returns(marshaller.Object);
        }

        [Test]
        [Ignore]
        public void ShouldSetMarshallerToResult() {}

        [Test]
        public void ShouldReturnNotAcceptableWhenMediaTypeIsNotSupported()
        {
            acceptHeader.Setup(f => f.GetMediaType(It.IsAny<string>())).Throws(new AcceptHeaderNotSupportedException());
            requestInfo.Setup(ah => ah.GetAcceptHeaderIn(context)).Returns("some-crazy-media-type");

            var filter = new ActAsRestfulie(acceptHeader.Object, contentType.Object, requestInfo.Object);
            
            filter.OnActionExecuting(context);

            Assert.IsTrue(context.Result is NotAcceptable);
        }

        [Test]
        public void ShouldUnmarshallResource()
        {
            var filter = new ActAsRestfulie(acceptHeader.Object, contentType.Object, requestInfo.Object)
                             {
                                 Name = "Resource",
                                 Type = typeof (SomeResource)
                             };

            var resource = new SomeResource {Amount = 123, Name = "Some name"};

            unmarshaller.Setup(u => u.ToResource(It.IsAny<string>(), typeof (SomeResource))).Returns(resource);
            contentType.Setup(m => m.GetMediaType(It.IsAny<string>())).Returns(mediaType.Object);

            filter.OnActionExecuting(context);

            Assert.AreEqual(resource, context.ActionParameters["Resource"]);
        }

        [Test]
        public void ShouldReturnUnsupportedMediaTypeWhenContentTypeIsNotSupported()
        {
            contentType.Setup(f => f.GetMediaType(It.IsAny<string>())).Throws(new ContentTypeNotSupportedException());
            requestInfo.Setup(ah => ah.GetContentTypeIn(context)).Returns("some-crazy-media-type");

            var filter = new ActAsRestfulie(acceptHeader.Object, contentType.Object, requestInfo.Object)
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
            unmarshaller.Setup(u => u.ToResource(It.IsAny<string>(), It.IsAny<Type>())).Throws(
                new UnmarshallingException("message"));

            contentType.Setup(f => f.GetMediaType(It.IsAny<string>())).Returns(mediaType.Object);

            var filter = new ActAsRestfulie(acceptHeader.Object, contentType.Object, requestInfo.Object)
            {
                Name = "Resource",
                Type = typeof(SomeResource)
            };

            filter.OnActionExecuting(context);

            Assert.IsTrue(context.Result is BadRequest);
        }

        [Test]
        public void ShouldReturnInternalServerErrorWhenMarshallingFails()
        {
            marshaller.Setup(u => u.Build(It.IsAny<ControllerContext>(), It.IsAny<IBehaveAsResource>(), It.IsAny<ResponseInfo>())).Throws(
                new Exception());

            acceptHeader.Setup(f => f.GetMediaType(It.IsAny<string>())).Returns(mediaType.Object);

            var filter = new ActAsRestfulie(acceptHeader.Object, contentType.Object, requestInfo.Object)
            {
                Name = "Resource",
                Type = typeof(SomeResource)
            };

            filter.OnActionExecuting(context);

            Assert.IsTrue(context.Result is InternalServerError);
        }
    }
}
