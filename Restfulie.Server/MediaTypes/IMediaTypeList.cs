using System.Collections.Generic;

namespace Restfulie.Server.MediaTypes
{
    public interface IMediaTypeList
    {
        IEnumerable<IMediaType> MediaTypes { get; }
        IMediaType Find(string name);
    	IMediaType Find<T>();
        IMediaType Default { get; }
    	void SetDefault(IMediaType defaultMediaType);
    }
}
