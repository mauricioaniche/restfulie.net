using System;
using System.Web.Script.Serialization;

namespace Restfulie.Server.Marshalling.Serializers.Json
{
    public class JsonSerializer : IResourceSerializer
    {
        public string Serialize(object resource)
        {
            var serializer = new JavaScriptSerializer();

            return serializer.Serialize(resource);
        }
    }
}
