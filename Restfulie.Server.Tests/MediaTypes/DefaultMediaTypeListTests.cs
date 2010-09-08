using System.Collections.Generic;
using System.Linq;
using Moq;
using NUnit.Framework;
using Restfulie.Server.Configuration;
using Restfulie.Server.MediaTypes;
using Should;

namespace Restfulie.Server.Tests.MediaTypes
{
    [TestFixture]
    public class DefaultMediaTypeListTests
    {
        private IList<IMediaType> anyList;
        private IMediaType anyMediaType;
    	private DefaultMediaTypeList _list;

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

            anyMediaType = new HTML();

        	_list = new DefaultMediaTypeList(anyList, anyMediaType);
        }

        [Test]
        public void ShouldFindByName()
        {
            var mediaType = _list.Find("application/xml");
        	mediaType.ShouldBeType<XmlAndHypermedia>();
        }

        [Test]
        public void ShouldReturnNullIfDoesNotFind()
        {
            var mediaType = _list.Find("crazy-media-type");
			mediaType.ShouldBeNull();
        }

        [Test]
        public void ShouldFindByNameJsonMediaType()
        {
            var mediaType = _list.Find("application/json");
        	mediaType.ShouldBeType<JsonAndHypermedia>();
        }

		[Test]
		public void ShouldBeAbleToChangeDefaultMediaType()
		{
			var newDefault = anyList.Last();
			_list.SetDefault(newDefault);
			_list.Default.ShouldEqual(newDefault);
		}

		[Test]
		public void ShouldFindByTypeUsingGenerics()
		{
			_list.Find<XmlAndHypermedia>().ShouldBeType<XmlAndHypermedia>();
		}

		[Test]
		public void ShouldThrowRestfulieConfigurationExceptionWhenTriesToSetAsDefaultANonRegisteredMediaType()
		{
			TestDelegate setNonRegisteredTypeAsDefault = SetNonRegisteredTypeAsDefault;
			Assert.Throws(typeof (RestfulieConfigurationException), setNonRegisteredTypeAsDefault);
		}

		private void SetNonRegisteredTypeAsDefault()
		{
			var newDefault = new Mock<IMediaType>().Object;
			_list.SetDefault(newDefault);
		}
    
    }
}
