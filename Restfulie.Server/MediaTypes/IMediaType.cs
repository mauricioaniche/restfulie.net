using Restfulie.Server.Configuration;
using Restfulie.Server.Marshalling;
using Restfulie.Server.Unmarshalling;

namespace Restfulie.Server.MediaTypes
{
    public interface IMediaType
    {
        string[] Synonyms { get; }
        IResourceMarshaller GetMarshaller(IRestfulieConfiguration config);
        IResourceUnmarshaller GetUnmarshaller(IRestfulieConfiguration config);
    }
}
