using Restfulie.Server.Marshalling;
using Restfulie.Server.Marshalling.Serializers;
using Restfulie.Server.Marshalling.UrlGenerators;
using Restfulie.Server.Unmarshalling;
using Restfulie.Server.Unmarshalling.Deserializers;

namespace Restfulie.Server.MediaTypes
{
    public class VendorizedMediaType : IMediaType
    {

        public VendorizedMediaType(string format)
        {
            Synonyms = new[] {format};
        }

        public IResourceSerializer Serializer { get; set; }
        public IHypermediaInserter Hypermedia { get; set; }
        public IResourceDeserializer Deserializer { get; set; }

        public string[] Synonyms
        {
            get; private set;
        }

        public IResourceMarshaller Marshaller
        {
            get
            {
                return
                    new RestfulieMarshaller(new Relations(new AspNetMvcUrlGenerator()),
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
