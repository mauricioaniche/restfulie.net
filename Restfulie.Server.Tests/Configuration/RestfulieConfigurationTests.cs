using System;
using NUnit.Framework;
using Restfulie.Server.Configuration;
using Restfulie.Server.Marshalling;
using Restfulie.Server.Marshalling.Serializers.XmlAndHypermedia;
using Restfulie.Server.MediaTypes;
using Restfulie.Server.Unmarshalling;
using Restfulie.Server.Unmarshalling.Deserializers.Xml;
using System.Linq;
using Should;

namespace Restfulie.Server.Tests.Configuration
{
	[TestFixture]
	public class RestfulieConfigurationTests
	{
		private RestfulieConfiguration _config;
		

		[SetUp]
		public void SetUp()
		{
			_config = new RestfulieConfiguration();
		}

		[Test]
		public void ShouldRegisterSerializerAndDeserializerForAMediaType()
		{

			_config.Register<XmlAndHypermedia>(new Driver(new XmlSerializer(), new XmlHypermediaInjector(), new XmlDeserializer()));

			var mediaType = _config.MediaTypeList.Find("application/xml");
			mediaType.Driver.Serializer.ShouldBeType<XmlSerializer>();
			mediaType.Driver.Deserializer.ShouldBeType<XmlDeserializer>();
			mediaType.Driver.HypermediaInjector.ShouldBeType<XmlHypermediaInjector>();
		}

		[Test]
		public void ShouldRegisterAVendorizedMediaType()
		{
			
			_config.RegisterVendorized("some-vendor-media-type", new Driver(new XmlSerializer(), new XmlHypermediaInjector(), new XmlDeserializer()));

			var mediaType = _config.MediaTypeList.Find("some-vendor-media-type");
			mediaType.ShouldBeType<Vendorized>();
			mediaType.Synonyms.First().ShouldEqual("some-vendor-media-type");
		}

		[Test]
		public void ShouldRemoveAMediaType()
		{
			_config.Remove<XmlAndHypermedia>();
			_config.MediaTypeList.MediaTypes.Any(x => x.GetType() == typeof(XmlAndHypermedia)).ShouldBeFalse();
		}

		[Test]
		public void ShouldRemoveAMediaTypePassingAInstanceOfTheMediaTypeAsArgument()
		{
			var mediaTypeToRemove = _config.MediaTypeList.MediaTypes.First();
			_config.Remove(mediaTypeToRemove);
			_config.MediaTypeList.MediaTypes.ShouldNotContain(mediaTypeToRemove);
		}

		[Test]
		public void ShouldThrowRestfulieConfigurationExceptionWhenTriesToRemoveNonRegisteredMediaType()
		{
			TestDelegate removeNonRegistered = RemoveNonRegisteredMediaType;
			Assert.Throws(typeof (RestfulieConfigurationException), removeNonRegistered);
			
		}

		[Test]
		public void ShouldThrowRestfulieConfigurationExceptionWhenTriesToRemoveAllMediaTypeRegistered()
		{
			TestDelegate removeAllMediaTypes = RemoveAllMediaTypes;
			Assert.Throws(typeof(RestfulieConfigurationException), removeAllMediaTypes);
		}

		[Test]
		public void ShouldSetDefaultMediaType()
		{
			var jsonHypermedia = _config.MediaTypeList.Find<JsonAndHypermedia>();

			_config.SetDefaultMediaType<JsonAndHypermedia>();

			_config.MediaTypeList.Default.ShouldEqual(jsonHypermedia);
		}


		[Test]
		public void ShouldSetFirstMediaTypeInListAsDefaultWhenRemovingTheDefaultMediaType()
		{
			_config.SetDefaultMediaType<JsonAndHypermedia>();
            var firstMediaType = _config.MediaTypeList.MediaTypes.First(x => !x.Equals(_config.MediaTypeList.Default));
            
			_config.Remove<JsonAndHypermedia>();
			
			_config.MediaTypeList.Default.ShouldEqual(firstMediaType);
		}

		private void RemoveNonRegisteredMediaType()
		{
			_config.Remove<NonRegisteredMediaType>();
		}

		private void RemoveAllMediaTypes()
		{
			while(_config.MediaTypeList.MediaTypes.Any())
			{
			    var first = _config.MediaTypeList.MediaTypes.First();
			    _config.Remove(first);
			}
		}
	}

	public class NonRegisteredMediaType : MediaType
	{
		public override string[] Synonyms
		{
			get { throw new NotImplementedException(); }
		}

		public override IResourceMarshaller BuildMarshaller()
		{
			throw new NotImplementedException();
		}

		public override IResourceUnmarshaller BuildUnmarshaller()
		{
			throw new NotImplementedException();
		}
	}
}
