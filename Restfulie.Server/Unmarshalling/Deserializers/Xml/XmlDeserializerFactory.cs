using System;

namespace Restfulie.Server.Unmarshalling.Deserializers.Xml
{
    public class XmlDeserializerFactory : IDeserializerFactory
    {
        public IResourceDeserializer Create()
        {
            return new XmlDeserializer();
        }
    }
}
