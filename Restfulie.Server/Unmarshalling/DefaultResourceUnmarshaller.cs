using Restfulie.Server.Unmarshalling.Deserializers;

namespace Restfulie.Server.Unmarshalling
{
    public class DefaultResourceUnmarshaller
    {
        private readonly IResourceDeserializer deserializer;

        public DefaultResourceUnmarshaller(IResourceDeserializer deserializer)
        {
            this.deserializer = deserializer;
        }

        public T ToResource<T>(string xml) where T : IBehaveAsResource
        {
            return deserializer.Deserialize<T>(xml);
        }
    }
}
