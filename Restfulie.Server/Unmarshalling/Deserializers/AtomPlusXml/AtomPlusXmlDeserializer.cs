using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;
using Restfulie.Server.Extensions;

namespace Restfulie.Server.Unmarshalling.Deserializers.AtomPlusXml
{
    public class AtomPlusXmlDeserializer : IResourceDeserializer
    {
        public IBehaveAsResource DeserializeResource(string content, Type objectType)
        {
            var xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(content);

            var entryContent = xmlDocument.DocumentElement.GetElementsByTagName("content")[0];

            var deserializer = new XmlSerializer(objectType);
            return (IBehaveAsResource)deserializer.Deserialize(entryContent.InnerText.AsStream());
        }

        public IBehaveAsResource[] DeserializeList(string content, Type objectType)
        {
            var xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(content);
            
            var elements = xmlDocument.GetElementsByTagName("entry");

            var resources = Array.CreateInstance(objectType, elements.Count);
            for(var i = 0; i < elements.Count; i++)
            {
                var element = elements[i];
                resources.SetValue(DeserializeResource(element.OuterXml, objectType), i);
            }

            return (IBehaveAsResource[])resources;
        }
    }
}
