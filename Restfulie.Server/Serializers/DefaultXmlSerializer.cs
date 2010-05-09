using System;
using System.IO;
using System.Xml.Serialization;

namespace Restfulie.Server.Serializers
{
    public class DefaultXmlSerializer : ISerializer
    {
        private MemoryStream stream;

        public DefaultXmlSerializer()
        {
            stream = new MemoryStream();
        }

        public string Serialize(IBehaveAsResource resource)
        {
            return WriteTheXmlBasedOn(resource).AndThenRead();
        }

        private DefaultXmlSerializer WriteTheXmlBasedOn(IBehaveAsResource resource)
        {
            var s = new XmlSerializer(resource.GetType());
            s.Serialize(stream, resource);
            return this;
        }

        private string AndThenRead()
        {
            string xml;
            using (var reader = new StreamReader(stream))
            {
                stream.Seek(0, SeekOrigin.Begin);
                xml = reader.ReadToEnd();
            }

            return xml;
        }
    }
}
