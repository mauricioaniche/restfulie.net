using System;
using System.Collections.Generic;
using System.Linq;

namespace Restfulie.Server.MediaTypes
{
    public class DefaultMediaTypeList : IMediaTypeList
    {
        public IEnumerable<IMediaType> MediaTypes { get; private set; }

    	public IMediaType Default { get; private set; }

    	public DefaultMediaTypeList(IEnumerable<IMediaType> mediaTypes, IMediaType defaultMediaType)
        {
            MediaTypes = mediaTypes;
            Default = defaultMediaType;
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
			Default = defaultMediaType;
		}

    }
}
