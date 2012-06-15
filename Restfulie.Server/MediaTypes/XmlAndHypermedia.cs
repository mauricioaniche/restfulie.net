using Restfulie.Server.Marshalling.Serializers.XmlAndHypermedia;
using Restfulie.Server.Unmarshalling.Deserializers.Xml;

namespace Restfulie.Server.MediaTypes
{
    public class XmlAndHypermedia : RestfulieMediaType
    {
        public XmlAndHypermedia()
        {
            Driver = new Driver(new XmlSerializer(), new XmlHypermediaInjector(), new XmlDeserializer());
        }

        public override string[] Synonyms
        {
            get { return new[] {"application/xml", "text/xml"}; }
        }
    }
}