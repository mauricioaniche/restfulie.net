using System;
using System.Xml.Serialization;
using Restfulie.Server.Extensions;

namespace Restfulie.Server.Unmarshalling.Deserializers.Xml
{
    public class XmlDeserializer : IResourceDeserializer
    {
        #region IResourceDeserializer Members

        public object Deserialize(string xml, Type objectType)
        {
            var serializer = new XmlSerializer(objectType);
            return serializer.Deserialize(xml.AsStream());
        }

        #endregion
    }
}