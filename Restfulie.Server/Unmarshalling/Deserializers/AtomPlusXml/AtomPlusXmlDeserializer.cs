using System;
using System.Xml;
using System.Xml.Serialization;
using Restfulie.Server.Extensions;

namespace Restfulie.Server.Unmarshalling.Deserializers.AtomPlusXml
{
    public class AtomPlusXmlDeserializer : IResourceDeserializer
    {
        #region IResourceDeserializer Members

        public object Deserialize(string content, Type objectType)
        {
            var xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(content);

            var contents = xmlDocument.DocumentElement.GetElementsByTagName("content");

            if (objectType.IsAResource())
                return ToObject(contents[0].InnerText, objectType);

            if (objectType.IsAListOfResources())
            {
                var list = Array.CreateInstance(objectType.GetElementType(), contents.Count);
                for (var i = 0; i < contents.Count; i++)
                    list.SetValue(ToObject(contents[i].InnerText, objectType.GetElementType()), i);

                return list;
            }

            return null;
        }

        #endregion

        private static object ToObject(string xml, Type objectType)
        {
            var deserializer = new XmlSerializer(objectType);
            return deserializer.Deserialize(xml.AsStream());
        }
    }
}