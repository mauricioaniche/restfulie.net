using System.Collections.Generic;
using NUnit.Framework;
using Restfulie.Server.MediaTypes;

namespace Restfulie.Server.Tests.MediaTypes
{
    [TestFixture]
    public class DefaultMediaTypeListTests
    {
        private IList<IMediaType> anyList;

        [SetUp]
        public void SetUp()
        {
            anyList = new List<IMediaType>
                          {
                              new HTML(),
                              new AtomPlusXml(),
                              new XmlAndHypermedia()
                          };
        }

        [Test]
        public void ShouldFindByName()
        {
            var mediaType = new DefaultMediaTypeList(anyList).Find("application/xml");
            Assert.IsTrue(mediaType is XmlAndHypermedia);
        }

        [Test]
        public void ShouldReturnNullIfDoesNotFind()
        {
            var mediaType = new DefaultMediaTypeList(anyList).Find("crazy-media-type");
            Assert.IsNull(mediaType);
        }
    }
}
