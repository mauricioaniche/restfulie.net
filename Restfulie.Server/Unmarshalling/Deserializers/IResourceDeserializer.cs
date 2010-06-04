using System;

namespace Restfulie.Server.Unmarshalling.Deserializers
{
    public interface IResourceDeserializer
    {
        IBehaveAsResource DeserializeResource(string xml, Type objectType);
        IBehaveAsResource[] DeserializeList(string xml, Type objectType);
    }
}
