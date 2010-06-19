using Restfulie.Server.Marshalling;
using Restfulie.Server.Marshalling.Serializers;
using Restfulie.Server.Marshalling.UrlGenerators;
using Restfulie.Server.Unmarshalling;
using Restfulie.Server.Unmarshalling.Deserializers;

namespace Restfulie.Server.MediaTypes
{
    public abstract class RestfulieMediaType : IMediaType
    {
        public IResourceSerializer Serializer { get; set; }
        public IHypermediaInserter Hypermedia { get; set; }
        public IResourceDeserializer Deserializer { get; set; }

        public abstract string[] Synonyms { get; }

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
