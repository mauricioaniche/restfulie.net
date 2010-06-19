using System;
using Newtonsoft.Json;

namespace Restfulie.Server.Marshalling.Serializers.Json
{
    public class JsonSerializer : IResourceSerializer
    {
        public string Serialize(object resource)
        {
            return  JsonConvert.SerializeObject(resource);
        }
    }
}
