using Restfulie.Server.Marshalling.Serializers;
using Restfulie.Server.Unmarshalling.Deserializers;

namespace Restfulie.Server.MediaTypes
{
    public interface IDriver
    {
        IResourceSerializer Serializer { get; set; }
        IHypermediaInserter HypermediaInserter { get; set; }
        IResourceDeserializer Deserializer { get; set; }
    }
}
