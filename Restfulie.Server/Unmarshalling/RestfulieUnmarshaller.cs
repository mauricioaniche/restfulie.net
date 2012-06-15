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

        #region IResourceUnmarshaller Members

        public object Build(string xml, Type objectType)
        {
            try
            {
                return string.IsNullOrEmpty(xml) ? null : deserializer.Deserialize(xml, objectType);
            } catch (Exception e)
            {
                throw new UnmarshallingException(e.Message);
            }
        }

        #endregion
    }
}