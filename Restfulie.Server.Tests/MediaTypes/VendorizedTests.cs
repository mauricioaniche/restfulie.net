using System.Linq;
using NUnit.Framework;
using Restfulie.Server.MediaTypes;

namespace Restfulie.Server.Tests.MediaTypes
{
    [TestFixture]
    public class VendorizedTests
    {
        [Test]
        public void ShouldSetTheSynonym()
        {
            var mediaType = new Vendorized("some-vendor-media-type");

            Assert.AreEqual("some-vendor-media-type", mediaType.Synonyms.First());
        }
    }
}
