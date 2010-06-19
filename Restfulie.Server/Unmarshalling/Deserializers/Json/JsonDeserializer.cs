using System;
using Newtonsoft.Json;

namespace Restfulie.Server.Unmarshalling.Deserializers.Json
{
    public class JsonDeserializer : IResourceDeserializer
    {
        public object Deserialize(string content, Type objectType)
        {
            return JsonConvert.DeserializeObject(content, objectType);
        }
    }
}
