using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace Restfulie.Server.Unmarshalling.Deserializers
{
    public class XmlDeserializer : IResourceDeserializer
    {
        public T Deserialize<T>(string xml) where T : IBehaveAsResource
        {
            var serializer = new XmlSerializer(typeof(T));
            return (T)serializer.Deserialize(AStreamWith(xml));
        }

        private Stream AStreamWith(string xml)
        {
            var byteArray = new List<byte>();
            foreach(var s in xml)
            {
                byteArray.Add(Convert.ToByte(s));
            }

            return new MemoryStream(byteArray.ToArray());
        }
    }
}
