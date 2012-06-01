using System;

namespace Restfulie.Server.Unmarshalling.Deserializers
{
    public interface IResourceDeserializer
    {
        object Deserialize(string content, Type objectType);
    }
}