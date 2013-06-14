using Moq;
using NUnit.Framework;
using Restfulie.Server.MediaTypes;
using Restfulie.Server.Results;
using Restfulie.Server.Results.Decorators;

namespace Restfulie.Server.Tests.Results
{
    [TestFixture]
    public class PreconditionFailedTests
    {
        private PreconditionFailed result;
        private Mock<IMediaType> mediaType;

        [SetUp]
        public void SetUp()
        {
            mediaType = new Mock<IMediaType>();
            mediaType.SetupGet(mt => mt.Synonyms).Returns(new[] { "media-type" });
        }

        [Test]
        public void ShouldSetStatusCode()
        {
            result = new PreconditionFailed()
            {
                MediaType = mediaType.Object,
            };

            Assert.That(result.GetDecorators().Contains(typeof(StatusCode)));
        }

        [Test]
        public void ShouldSetResponseText()
        {
            result = new PreconditionFailed(new {});

            Assert.That(result.Model, Is.Not.Null);
        }

    }
}
