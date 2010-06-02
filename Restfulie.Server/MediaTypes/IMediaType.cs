using Restfulie.Server.Marshalling;
using Restfulie.Server.Unmarshalling;

namespace Restfulie.Server.MediaTypes
{
    public interface IMediaType
    {
        string Name { get; }
        string[] Acronyms { get; }
        IResourceMarshaller Marshaller { get; }
        IResourceUnmarshaller Unmarshaller { get; }
    }
}
