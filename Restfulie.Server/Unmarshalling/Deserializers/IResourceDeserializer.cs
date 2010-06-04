using System;

namespace Restfulie.Server.Unmarshalling.Deserializers
{
    public interface IResourceDeserializer
    {
        IBehaveAsResource DeserializeResource(string content, Type objectType);
        IBehaveAsResource[] DeserializeList(string content, Type objectType);
    }
}
