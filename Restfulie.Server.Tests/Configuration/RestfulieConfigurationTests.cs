using NUnit.Framework;
using Restfulie.Server.Configuration;
using Restfulie.Server.Marshalling.Serializers.XmlAndHypermedia;
using Restfulie.Server.MediaTypes;
using Restfulie.Server.Unmarshalling.Deserializers.Xml;
using System.Linq;
using Should;

namespace Restfulie.Server.Tests.Configuration
{
	[TestFixture]
	public class RestfulieConfigurationTests
	{
		[Test]
		public void ShouldRegisterSerializerAndDeserializerForAMediaType()
		{
			var config = new RestfulieConfiguration();

			config.Register<XmlAndHypermedia>(new Driver(new XmlSerializer(), new XmlHypermediaInjector(), new XmlDeserializer()));

			var mediaType = config.MediaTypes.Find("application/xml");
			mediaType.Driver.Serializer.ShouldBeType<XmlSerializer>();
			mediaType.Driver.Deserializer.ShouldBeType<XmlDeserializer>();
			mediaType.Driver.HypermediaInjector.ShouldBeType<XmlHypermediaInjector>();
		}

		[Test]
		public void ShouldRegisterAVendorizedMediaType()
		{
			var config = new RestfulieConfiguration();
			config.RegisterVendorized("some-vendor-media-type", new Driver(new XmlSerializer(), new XmlHypermediaInjector(), new XmlDeserializer()));

			var mediaType = config.MediaTypes.Find("some-vendor-media-type");
			mediaType.ShouldBeType<Vendorized>();
			mediaType.Synonyms.First().ShouldEqual("some-vendor-media-type");
		}

		[Test]
		public void ShouldRemoveAMediaType()
		{
			var config = new RestfulieConfiguration();
			config.Remove<XmlAndHypermedia>();

		}
	}

	[TestFixture]
	public class RestfulieConfigurationTest_for_default_media_type_removal_without_specifing_a_new_default
	{
		private RestfulieConfiguration _configuration;

		[Test]
		public void When_the_default_media_type_is_removed_should_throw_configuration_exception()
		{
			_configuration = new RestfulieConfiguration();
			TestDelegate removeWithoutDefault = RemoveWithoutDefault;
			Assert.Throws(typeof(RestfulieConfigurationException), removeWithoutDefault);
		}

		private void RemoveWithoutDefault()
		{
			_configuration.Remove<HTML>();
		}
	}

	[TestFixture]
	public class RestfulieConfigurationTest_for_default_media_type_removal_passing_new_default_media_type_object
	{
		private IMediaTypeList _mediaTypeList;
		private RestfulieConfiguration _configuration;

		[SetUp]
		public void When_MediaType_is_removed()
		{
			_configuration = new RestfulieConfiguration();
			_mediaTypeList = _configuration.MediaTypes;
			var newDefault = _configuration.MediaTypes.Find<XmlAndHypermedia>();
			_configuration.Remove<HTML>(newDefault);
		}

		[Test]
		public void MediaType_should_be_removed_from_list()
		{
			_mediaTypeList.Find<HTML>().ShouldBeNull();
		}

		[Test]
		public void The_default_MediaType_should_be_set()
		{
			_mediaTypeList.Default.ShouldBeType<XmlAndHypermedia>();
		}
	}

	[TestFixture]
	public class RestfulieConfigurationTest_for_default_media_type_removal_passing_new_type
	{
		private IMediaTypeList _mediaTypeList;
		private RestfulieConfiguration _configuration;

		[SetUp]
		public void When_MediaType_is_removed()
		{
			_configuration = new RestfulieConfiguration();
			_mediaTypeList = _configuration.MediaTypes;
			_configuration.Remove<HTML>(typeof(XmlAndHypermedia));
		}

		[Test]
		public void MediaType_should_be_removed_from_list()
		{
			_mediaTypeList.Find<HTML>().ShouldBeNull();
		}

		[Test]
		public void The_default_MediaType_should_be_set()
		{
			_mediaTypeList.Default.ShouldBeType<XmlAndHypermedia>();
		}
	}
}
