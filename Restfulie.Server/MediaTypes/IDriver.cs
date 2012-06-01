using Restfulie.Server.Marshalling.Serializers;
using Restfulie.Server.Unmarshalling.Deserializers;

namespace Restfulie.Server.MediaTypes
{
    public interface IDriver
    {
        IResourceSerializer Serializer { get; set; }
        IHypermediaInjector HypermediaInjector { get; set; }
        IResourceDeserializer Deserializer { get; set; }
    }
}