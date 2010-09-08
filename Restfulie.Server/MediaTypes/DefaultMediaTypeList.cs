using System;
using System.Collections.Generic;
using System.Linq;
using Restfulie.Server.Configuration;

namespace Restfulie.Server.MediaTypes
{
    public class DefaultMediaTypeList : IMediaTypeList
    {
        public IEnumerable<IMediaType> MediaTypes { get; private set; }

    	public IMediaType Default { get; private set; }

    	public DefaultMediaTypeList(IEnumerable<IMediaType> mediaTypes, IMediaType defaultMediaType)
        {
            MediaTypes = mediaTypes;
            SetDefault(defaultMediaType);
        }

    	public IMediaType Find(string format)
        {
        	return MediaTypes.FirstOrDefault(mediaType => mediaType.Synonyms.Any(format.Equals));
        }

    	public IMediaType Find<T>()
    	{
    		return MediaTypes.FirstOrDefault(type => type.GetType() == typeof(T));
    	}

    	public void SetDefault(IMediaType defaultMediaType)
		{
			if(!IsRegistered(defaultMediaType))
				throw new RestfulieConfigurationException(string.Format("Can't set type as default. {0} is not a registered media type.",defaultMediaType));
			Default = defaultMediaType;
		}

    	private bool IsRegistered(IMediaType mediaType)
    	{
    		return MediaTypes.Contains(mediaType);
    	}
    }
}
