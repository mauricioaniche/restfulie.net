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
            var media = FindOrCreate(typeof (T));
            media.Driver = driver;
        }

        public IMediaTypeList MediaTypes
        {
            get
            {
				if(_mediaTypeList == null)
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
    		var typeToRemove = Find(typeof (T));
    		var storeContainType = typeToRemove != null;
			if (storeContainType)
			{
				var isDefault = typeof(T) == MediaTypes.Default.GetType();
				if (isDefault)
					throw new RestfulieConfigurationException(string.Format("Can't remove the type {0}. The default media type can only be removed using Remove<TMediaTypeToRemove>(IMediaType defaultMediaType) or Remove<TMediaTypeToRemove>(Type defaultMediaType).", typeToRemove));
				_store.Remove(typeToRemove);
			}
    	}

		private IMediaType Find(Type mediaType)
		{
			return _store.Where(mt => mt.GetType() == mediaType).SingleOrDefault();
		}

    	public void Remove<TMediaTypeToRemove>(IMediaType defaultMediaType) where TMediaTypeToRemove : IMediaType
    	{
    		MediaTypes.SetDefault(defaultMediaType);
			Remove<TMediaTypeToRemove>();
    	}

    	public void Remove<TMediaTypeToRemove>(Type defaultMediaType) where TMediaTypeToRemove : IMediaType
		{
			var newDefault = FindOrCreate(defaultMediaType);
			MediaTypes.SetDefault(newDefault);
			Remove<TMediaTypeToRemove>();
		}
    }
}
