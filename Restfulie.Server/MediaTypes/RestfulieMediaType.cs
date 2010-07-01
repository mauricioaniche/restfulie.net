using Restfulie.Server.Marshalling;
using Restfulie.Server.Marshalling.UrlGenerators;
using Restfulie.Server.Unmarshalling;

namespace Restfulie.Server.MediaTypes
{
    public abstract class RestfulieMediaType : IMediaType
    {
        public IDriver Driver { get; set; }

        public abstract string[] Synonyms { get; }

        public IResourceMarshaller BuildMarshaller()
        {
            return
                new RestfulieMarshaller(new RelationsFactory(new AspNetMvcUrlGenerator()),
                                        Driver.Serializer,
                                        Driver.HypermediaInserter);
        }

        public IResourceUnmarshaller BuildUnmarshaller()
        {
            return new RestfulieUnmarshaller(Driver.Deserializer);
        }
    }
}
