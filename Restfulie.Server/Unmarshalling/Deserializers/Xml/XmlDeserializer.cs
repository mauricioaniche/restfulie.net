using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace Restfulie.Server.Unmarshalling.Deserializers.Xml
{
    public class XmlDeserializer : IResourceDeserializer
    {
        private Stream AStreamWith(string xml)
        {
            var byteArray = new List<byte>();
            foreach(var s in xml)
            {
                byteArray.Add(Convert.ToByte(s));
            }

            return new MemoryStream(byteArray.ToArray());
        }

        public IBehaveAsResource DeserializeResource(string xml, Type objectType)
        {
            var serializer = new XmlSerializer(objectType);
            return (IBehaveAsResource)serializer.Deserialize(AStreamWith(xml));
        }

        public IBehaveAsResource[] DeserializeList(string xml, Type objectType)
        {
            var document = new XmlDocument();
            document.LoadXml(xml);

            var resources = Array.CreateInstance(objectType, document.DocumentElement.ChildNodes.Count);

            for (var i = 0; i < document.DocumentElement.ChildNodes.Count; i++)
            {
                var node = document.DocumentElement.ChildNodes[i];
                resources.SetValue(DeserializeResource(node.OuterXml, objectType), i);
            }

            return (IBehaveAsResource[])resources;
        }
    }
}