using Restfulie.Server.Marshalling;
using Restfulie.Server.Marshalling.Serializers;
using Restfulie.Server.Unmarshalling;
using Restfulie.Server.Unmarshalling.Deserializers;

namespace Restfulie.Server.MediaTypes
{
    public interface IMediaType
    {
        string[] Synonyms { get; }
        IResourceMarshaller Marshaller { get; }
        IResourceUnmarshaller Unmarshaller { get; }
        IResourceSerializer Serializer { get;  set; }
        IHypermediaInserter Hypermedia { get;  set; }
        IResourceDeserializer Deserializer { get;  set; }
    }
}
