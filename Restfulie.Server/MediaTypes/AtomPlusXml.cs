using Restfulie.Server.Marshalling.Serializers.AtomPlusXml;
using Restfulie.Server.Unmarshalling.Deserializers.AtomPlusXml;

namespace Restfulie.Server.MediaTypes
{
    public class AtomPlusXml : RestfulieMediaType
    {
        public AtomPlusXml()
        {
            Driver = new Driver(new AtomPlusXmlSerializer(), new AtomPlusXmlHypermediaInjector(), new AtomPlusXmlDeserializer());
        }

        public override string[] Synonyms
        {
            get { return new[] {"application/atom+xml", "atom+xml"}; }
        }
    }
}