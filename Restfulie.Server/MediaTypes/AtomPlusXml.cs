using Restfulie.Server.Marshalling.Serializers.AtomPlusXml;
using Restfulie.Server.Unmarshalling.Deserializers.AtomPlusXml;

namespace Restfulie.Server.MediaTypes
{
    public class AtomPlusXml : RestfulieMediaType
    {
        public AtomPlusXml()
        {
            Serializer = new AtomPlusXmlSerializer();
            Deserializer = new AtomPlusXmlDeserializer();
            Hypermedia = new AtomPlusXmlHypermediaInserter();
        }

        public override string[] Synonyms
        {
            get { return new[] {"application/atom+xml", "atom+xml"}; }
        }
    }
}
