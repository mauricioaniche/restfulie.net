using System;
using Restfulie.Server.Unmarshalling.Deserializers;

namespace Restfulie.Server.Unmarshalling
{
    public class DefaultResourceUnmarshaller : IResourceUnmarshaller
    {
        private readonly IResourceDeserializer deserializer;

        public DefaultResourceUnmarshaller(IResourceDeserializer deserializer)
        {
            this.deserializer = deserializer;
        }

        public IBehaveAsResource ToResource(string xml, Type objectType)
        {
            return string.IsNullOrEmpty(xml) ? null : deserializer.Deserialize(xml, objectType);
        }
    }
}
