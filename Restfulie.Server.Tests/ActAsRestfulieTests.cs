using System.Web.Mvc;
using Moq;
using NUnit.Framework;
using Restfulie.Server.Exceptions;
using Restfulie.Server.Marshalling;
using Restfulie.Server.Negotitation;
using Restfulie.Server.Results;
using Restfulie.Server.Tests.Fixtures;

namespace Restfulie.Server.Tests
{
    [TestFixture]
    public class ActAsRestfulieTests
    {
        [Test]
        public void ShouldReturnNotAcceptableWhenMediaTypeIsNotSupported()
        {
            var context = new ActionExecutingContext();

            var marshaller = new Mock<IRepresentationFactory>();
            marshaller.Setup(f => f.BasedOnMediaType(It.IsAny<string>())).Throws(new MediaTypeNotSupportedException());

            var acceptHeader = new Mock<IAcceptHeaderFinder>();
            acceptHeader.Setup(ah => ah.FindIn(context)).Returns("some-crazy-media-type");

            var filter = new ActAsRestfulie(marshaller.Object, acceptHeader.Object);
            
            filter.OnActionExecuting(context);

            Assert.IsTrue(context.Result is NotAcceptable);
        }

        [Test]
        [Ignore]
        public void ShouldUnmarshallResource()
        {
            var context = new ActionExecutingContext();

            var marshaller = new Mock<IRepresentationFactory>();
            var acceptHeader = new Mock<IAcceptHeaderFinder>();

            var filter = new ActAsRestfulie(marshaller.Object, acceptHeader.Object)
                             {
                                 Name = "Resource",
                                 Type = typeof (SomeResource)
                             };

            filter.OnActionExecuting(context);

            Assert.IsNotNull(context.ActionParameters["Resource"]);
        }
    }
}
