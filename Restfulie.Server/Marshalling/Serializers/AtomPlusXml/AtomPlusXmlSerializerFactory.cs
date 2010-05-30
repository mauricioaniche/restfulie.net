using System;

namespace Restfulie.Server.Marshalling.Serializers.AtomPlusXml
{
    public class AtomPlusXmlSerializerFactory : ISerializerFactory
    {
        public IResourceSerializer Create()
        {
            return new AtomPlusXmlSerializer();
        }
    }
}
