using System.Collections.Generic;
using Moq;
using NUnit.Framework;
using Restfulie.Server.MediaTypes;
using Should;

namespace Restfulie.Server.Tests.MediaTypes
{
    [TestFixture]
    public class DefaultMediaTypeListTests
    {
        private IList<IMediaType> anyList;
        private IMediaType anyMediaType;

        [SetUp]
        public void SetUp()
        {
            anyList = new List<IMediaType>
                          {
                              new HTML(),
                              new AtomPlusXml(),
                              new XmlAndHypermedia(),
                              new JsonAndHypermedia()
                          };

            anyMediaType = new Mock<IMediaType>().Object;
        }

        [Test]
        public void ShouldFindByName()
        {
            var mediaType = new DefaultMediaTypeList(anyList, anyMediaType).Find("application/xml");
            Assert.IsTrue(mediaType is XmlAndHypermedia);
        }

        [Test]
        public void ShouldReturnNullIfDoesNotFind()
        {
            var mediaType = new DefaultMediaTypeList(anyList, anyMediaType).Find("crazy-media-type");
            Assert.IsNull(mediaType);
        }

        [Test]
        public void ShouldFindByNameJsonMediaType()
        {
            var mediaType = new DefaultMediaTypeList(anyList, anyMediaType).Find("application/json");
            Assert.IsTrue(mediaType is JsonAndHypermedia);
        }

		[Test]
		public void Should_be_able_to_change_default_media_type()
		{
			var newDefault = new Mock<IMediaType>().Object;
			var list = new DefaultMediaTypeList(anyList, anyMediaType);
			list.SetDefault(newDefault);
			list.Default.ShouldEqual(newDefault);
		}

		[Test]
		public void ShouldFindByType()
		{
			var list = new DefaultMediaTypeList(anyList, anyMediaType);
			list.Find<XmlAndHypermedia>().ShouldBeType<XmlAndHypermedia>();
		}
    }
}
