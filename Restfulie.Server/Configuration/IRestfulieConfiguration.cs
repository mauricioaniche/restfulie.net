using Restfulie.Server.Marshalling.Serializers;
using Restfulie.Server.MediaTypes;
using Restfulie.Server.Unmarshalling.Deserializers;

namespace Restfulie.Server.Configuration
{
    public interface IRestfulieConfiguration
    {
        void Register<T>(IResourceSerializer serializer, IResourceDeserializer deserializer) where T : IMediaType;

        IMediaTypeList MediaTypes { get; }
    }
}