using Restfulie.Server.Marshalling;
using Restfulie.Server.Unmarshalling;

namespace Restfulie.Server.MediaTypes
{
    public interface IMediaType
    {
        string[] Synonyms { get; }
        IResourceMarshaller BuildMarshaller();
        IResourceUnmarshaller BuildUnmarshaller();
        IDriver Driver { get; set; }
    }
}
