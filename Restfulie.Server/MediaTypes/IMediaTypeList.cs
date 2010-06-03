using System.Collections.Generic;

namespace Restfulie.Server.MediaTypes
{
    public interface IMediaTypeList
    {
        IEnumerable<IMediaType> MediaTypes { get; }
        IMediaType Find(string name);
        IMediaType Default { get; }
    }
}
