using Restfulie.Server.Marshalling.UrlGenerators;
using Restfulie.Server.Negotitation;

namespace Restfulie.Server.Marshalling
{
    public class DefaultRepresentationFactory : IRepresentationFactory
    {
        public IResourceRepresentation BasedOnMediaType(string mediaType)
        {
            return new DefaultRepresentation(
                new Relations(new AspNetMvcUrlGenerator()), 
                new AcceptHeaderToSerializer().For(mediaType),
                new DefaultInflections());
        }
    }
}