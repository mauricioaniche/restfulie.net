using NUnit.Framework;
using Restfulie.Server.MediaTypes;

namespace Restfulie.Server.Tests.MediaTypes
{
    [TestFixture]
    public class DefaultMediaTypeListTests
    {
        [Test]
        public void ShouldFindByName()
        {
            var mediaType = new DefaultMediaTypeList().Find("application/xml");
            Assert.IsTrue(mediaType is XmlAndHypermedia);
        }

        [Test]
        public void ShouldReturnNullIfDoesNotFind()
        {
            var mediaType = new DefaultMediaTypeList().Find("crazy-media-type");
            Assert.IsNull(mediaType);
        }
    }
}
