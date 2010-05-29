using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace Restfulie.Server.Unmarshalling.Deserializers
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

        public IBehaveAsResource Deserialize(string xml, Type objectType)
        {
            var serializer = new XmlSerializer(objectType);
            return (IBehaveAsResource)serializer.Deserialize(AStreamWith(xml));
        }
    }
}
