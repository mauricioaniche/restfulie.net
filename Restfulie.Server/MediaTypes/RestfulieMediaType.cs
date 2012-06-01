using Restfulie.Server.Marshalling;
using Restfulie.Server.Marshalling.UrlGenerators;
using Restfulie.Server.Unmarshalling;

namespace Restfulie.Server.MediaTypes
{
    public abstract class RestfulieMediaType : MediaType
    {
        public override IResourceMarshaller BuildMarshaller()
        {
            return
                new RestfulieMarshaller(new RelationsFactory(new AspNetMvcUrlGenerator()),
                                        Driver.Serializer,
                                        Driver.HypermediaInjector);
        }

        public override IResourceUnmarshaller BuildUnmarshaller()
        {
            return new RestfulieUnmarshaller(Driver.Deserializer);
        }
    }
}