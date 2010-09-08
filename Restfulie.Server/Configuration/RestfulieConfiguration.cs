using System;
using System.Collections.Generic;
using Restfulie.Server.MediaTypes;
using System.Linq;

namespace Restfulie.Server.Configuration
{
	public class RestfulieConfiguration : IRestfulieConfiguration
	{
		private readonly IList<IMediaType> _store;
		private DefaultMediaTypeList _mediaTypeList;

		public RestfulieConfiguration()
		{
			_store = new List<IMediaType>
                        {
                            new XmlAndHypermedia(),
                            new AtomPlusXml(),
                            new HTML(),
                            new UrlEncoded(),
							new JsonAndHypermedia()
                        };
		}

		public void Register<T>(IDriver driver)
			where T : IMediaType
		{
			var media = FindOrCreate(typeof(T));
			media.Driver = driver;
		}

		public IMediaTypeList MediaTypeList
		{
			get
			{
				if (_mediaTypeList == null)
					_mediaTypeList = new DefaultMediaTypeList(_store, new HTML());
				return _mediaTypeList;
			}
		}

		public void RegisterVendorized(string format, IDriver driver)
		{
			var vendorizedMediaType = new Vendorized(format)
										  {
											  Driver = driver
										  };

			_store.Add(vendorizedMediaType);
		}

		private IMediaType FindOrCreate(Type mediaType)
		{
			var searchedMediaType = Find(mediaType);
			if (searchedMediaType == null)
			{
				searchedMediaType = (IMediaType)Activator.CreateInstance(mediaType);
				_store.Add(searchedMediaType);
			}

			return searchedMediaType;
		}



		public void Remove<T>() where T : IMediaType
		{
			var typeToRemove = Find(typeof(T));
			Remove(typeToRemove);
		}

		public void Remove(IMediaType mediaTypeToRemove)
		{
			var typeIsRegistered = mediaTypeToRemove != null;
			
			if (!typeIsRegistered)
				throw new RestfulieConfigurationException(string.Format("Can't remove {0}. Media Type is not registered.", mediaTypeToRemove));

			var isDefaultType = mediaTypeToRemove.Equals(MediaTypeList.Default);

			if (isDefaultType)
				throw new RestfulieConfigurationException(string.Format("Can't remove the type {0}. Type is the default media type. Set other Media Type as default via SetDefaultMediaType() before invoking the Remove method.", mediaTypeToRemove));
			
			_store.Remove(mediaTypeToRemove);

		}

		public void SetDefaultMediaType(IMediaType defaultMediaType)
		{
			MediaTypeList.SetDefault(defaultMediaType);
		}

		public void SetDefaultMediaType<TDefault>() where TDefault : IMediaType
		{
			var mediaType = Find(typeof(TDefault));
			if (mediaType == null)
				throw new RestfulieConfigurationException(string.Format("Media Type {0} can't be set as default. Media Type must be registered before set as default."));
			SetDefaultMediaType(mediaType);
		}

		private IMediaType Find(Type mediaType)
		{
			return _store.Where(mt => mt.GetType() == mediaType).SingleOrDefault();
		}
	}
}
