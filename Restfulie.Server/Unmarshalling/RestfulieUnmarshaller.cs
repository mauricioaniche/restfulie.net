using System;
using Restfulie.Server.Unmarshalling.Deserializers;

namespace Restfulie.Server.Unmarshalling
{
    public class RestfulieUnmarshaller : IResourceUnmarshaller
    {
        private readonly IResourceDeserializer deserializer;

        public RestfulieUnmarshaller(IResourceDeserializer deserializer)
        {
            this.deserializer = deserializer;
        }

        public IBehaveAsResource ToResource(string xml, Type objectType)
        {
            try
            {
                return string.IsNullOrEmpty(xml) ? null : deserializer.DeserializeResource(xml, objectType);
            }
            catch(Exception e)
            {
                throw new UnmarshallingException(e.Message);
            }
        }
    }
}
