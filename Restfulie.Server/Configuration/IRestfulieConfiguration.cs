using Restfulie.Server.Marshalling.Serializers;
using Restfulie.Server.MediaTypes;
using Restfulie.Server.Unmarshalling.Deserializers;

namespace Restfulie.Server.Configuration
{
    public interface IRestfulieConfiguration
    {
        void Register<T, T1, T2>() 
            where T : IMediaType 
            where T1 : IResourceSerializer 
            where T2 : IResourceDeserializer;

        IResourceSerializer GetSerializer<T>() where T : IMediaType;
        IResourceDeserializer GetDeserializer<T>() where T : IMediaType;
        void ClearDefaults();
    }
}