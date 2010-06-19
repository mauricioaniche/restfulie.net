using System;
using System.Web.Script.Serialization;

namespace Restfulie.Server.Marshalling.Serializers.Json
{
    public class JsonSerializer : IResourceSerializer
    {
        public string Serialize(object resource)
        {
            var jsonSerializer = new JavaScriptSerializer();

            return jsonSerializer.Serialize(resource);
        }
    }
}
