using Moq;
using NUnit.Framework;
using Restfulie.Server.MediaTypes;
using Restfulie.Server.Results;
using Restfulie.Server.Results.Decorators;

namespace Restfulie.Server.Tests.Results
{
    [TestFixture]
    public class NotModifiedTests
    {
        private NotModified result;

        [SetUp]
        public void SetUp()
        {
            var mediaType = new Mock<IMediaType>();
            mediaType.SetupGet(mt => mt.Synonyms).Returns(new[] { "media-type" });

            result = new NotModified
            {
                MediaType = mediaType.Object
            };
        }

        [Test]
        public void ShouldSetStatusCode()
        {
            Assert.That(result.GetDecorators().Contains(typeof(StatusCode)));
        }
    }
}
