using Restfulie.Server.Marshalling.Serializers.XmlAndHypermedia;
using Restfulie.Server.Unmarshalling.Deserializers.Xml;

namespace Restfulie.Server.MediaTypes
{
    public class Vendorized : RestfulieMediaType
    {
        private readonly string[] synonyms;

        public Vendorized(string format)
        {
            synonyms = new[] {format};
            Serializer = new XmlSerializer();
            Hypermedia = new XmlHypermediaInserter();
            Deserializer = new XmlDeserializer();
        }

        public override string[] Synonyms
        {
            get { return synonyms; }
        }
    }
}
