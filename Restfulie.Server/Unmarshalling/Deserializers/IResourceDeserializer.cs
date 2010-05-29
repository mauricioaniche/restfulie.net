using System;

namespace Restfulie.Server.Unmarshalling.Deserializers
{
    public interface IResourceDeserializer
    {
        IBehaveAsResource Deserialize(string xml, Type objectType);
    }
}
