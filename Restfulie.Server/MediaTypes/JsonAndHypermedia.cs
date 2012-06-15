using Restfulie.Server.Marshalling.Serializers.Json;
using Restfulie.Server.Unmarshalling.Deserializers.Json;

namespace Restfulie.Server.MediaTypes
{
    public class JsonAndHypermedia : RestfulieMediaType
    {
        public JsonAndHypermedia()
        {
            Driver = new Driver(new JsonSerializer(), new JsonHypermediaInjector(), new JsonDeserializer());
        }

        public override string[] Synonyms
        {
            get { return new[] {"application/json"}; }
        }
    }
}