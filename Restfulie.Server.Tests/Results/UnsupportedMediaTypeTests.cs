using Moq;
using NUnit.Framework;
using Restfulie.Server.MediaTypes;
using Restfulie.Server.Results;
using Restfulie.Server.Results.Decorators;
using Restfulie.Server.Results.Decorators.Holders;

namespace Restfulie.Server.Tests.Results
{
    [TestFixture]
    public class UnsupportedMediaTypeTests
    {
        private UnsupportedMediaType result;

        [SetUp]
        public void SetUp()
        {
            var mediaType = new Mock<IMediaType>();
            mediaType.SetupGet(mt => mt.Synonyms).Returns(new[] { "media-type" });

            result = new UnsupportedMediaType
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
    }
}
