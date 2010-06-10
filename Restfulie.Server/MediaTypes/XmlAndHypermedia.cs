using Restfulie.Server.Marshalling.Serializers.XmlAndHypermedia;
using Restfulie.Server.Unmarshalling.Deserializers.Xml;

namespace Restfulie.Server.MediaTypes
{
    public class XmlAndHypermedia : RestfulieMediaType
    {
        public XmlAndHypermedia()
        {
            Serializer = new XmlSerializer();
            Hypermedia = new XmlHypermediaInserter();
            Deserializer = new XmlDeserializer();
        }

        public override string[] Synonyms
        {
            get { return new[] {"application/xml", "text/xml" }; }
        }
    }
}
