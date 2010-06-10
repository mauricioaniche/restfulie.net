using Restfulie.Server.Marshalling;
using Restfulie.Server.Marshalling.Serializers;
using Restfulie.Server.Marshalling.Serializers.AtomPlusXml;
using Restfulie.Server.Marshalling.UrlGenerators;
using Restfulie.Server.Unmarshalling;
using Restfulie.Server.Unmarshalling.Deserializers;
using Restfulie.Server.Unmarshalling.Deserializers.AtomPlusXml;

namespace Restfulie.Server.MediaTypes
{
    public class AtomPlusXml : IMediaType
    {
        public IResourceSerializer Serializer { get; set; }
        public IHypermediaInserter Hypermedia { get; set; }
        public IResourceDeserializer Deserializer { get; set; }

        public AtomPlusXml()
        {
            Serializer = new AtomPlusXmlSerializer();
            Deserializer = new AtomPlusXmlDeserializer();
            Hypermedia = new AtomPlusXmlHypermediaInserter();
        }

        public string[] Synonyms
        {
            get { return new[] {"application/atom+xml", "atom+xml"}; }
        }

        public IResourceMarshaller Marshaller
        {
            get
            {
                return new RestfulieMarshaller(
                    new Relations(new AspNetMvcUrlGenerator()),
                    Serializer,
                    Hypermedia);
            }
        }

        public IResourceUnmarshaller Unmarshaller
        {
            get { return new RestfulieUnmarshaller(Deserializer); }
        }
    }
}
