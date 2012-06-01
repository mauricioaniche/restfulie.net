using System.Collections.Generic;

namespace Restfulie.Server.MediaTypes
{
    public interface IMediaTypeList
    {
        IEnumerable<IMediaType> MediaTypes { get; }
        IMediaType Default { get; }
        IMediaType Find(string name);
        IMediaType Find<T>();
        void SetDefault(IMediaType defaultMediaType);
    }
}