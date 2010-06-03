using Moq;
using Restfulie.Server.MediaTypes;
using NUnit.Framework;
using Restfulie.Server.Results.Decorators.Holders;

namespace Restfulie.Server.Tests.Results.Decorators.Holders
{
    [TestFixture]
    public class ResultDecoratorHolderFactoryTests
    {
        [Test]
        public void ShouldReturnAspNetHolderIfMediaTypeIsHTML()
        {
            var holder = new ResultDecoratorHolderFactory().BasedOn(new HTML());

            Assert.IsTrue(holder is AspNetMvcResultHolder);
        }

        [Test]
        public void ShouldReturnRestfulieHolderIfIsAnyOtherMediaType()
        {
            var anyMedia = new Mock<IMediaType>();
            var holder = new ResultDecoratorHolderFactory().BasedOn(anyMedia.Object);

            Assert.IsTrue(holder is RestfulieResultHolder);
        }
    }
}
