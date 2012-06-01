using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Restfulie.Server.Marshalling.Serializers.Json
{
    public class JsonSerializer : IResourceSerializer
    {
        public string Serialize(object resource)
        {
            var textWriter = new StringWriter();
            var jsonSerializer = new Newtonsoft.Json.JsonSerializer
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            };

            jsonSerializer.Converters.Add(new JavaScriptDateTimeConverter());

            jsonSerializer.Serialize(textWriter, resource);
            return textWriter.ToString();
        }
    }
}
