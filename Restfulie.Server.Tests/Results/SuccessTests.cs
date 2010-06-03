using Moq;
using NUnit.Framework;
using Restfulie.Server.MediaTypes;
using Restfulie.Server.Results;
using Restfulie.Server.Results.Decorators;
using Restfulie.Server.Results.Decorators.Holders;

namespace Restfulie.Server.Tests.Results
{
    [TestFixture]
    public class SuccessTests
    {
        private Success result;

        [SetUp]
        public void SetUp()
        {
            var mediaType = new Mock<IMediaType>();
            mediaType.SetupGet(mt => mt.Synonyms).Returns(new[] { "media-type" });

            result = new Success
            {
                MediaType = mediaType.Object,
                ResultHolder = new Mock<IResultDecoratorHolder>().Object
            };
        }

        [Test]
        public void ShouldSetStatusCode()
        {
            Assert.That(result.GetDecorators().Contains(typeof(StatusCode)));
        }

        [Test]
        public void ShouldSetContent()
        {
            Assert.That(result.GetDecorators().Contains(typeof(Content)));
        }

        [Test]
        public void ShouldSetContentType()
        {
            Assert.That(result.GetDecorators().Contains(typeof(ContentType)));
        }
    }
}
