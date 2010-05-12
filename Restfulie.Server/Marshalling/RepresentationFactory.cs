using Restfulie.Server.Marshalling.UrlGenerators;
using Restfulie.Server.Negotitation;

namespace Restfulie.Server.Marshalling
{
    public class RepresentationFactory
    {
        public IRepresentationBuilder BasedOnMediaType(string mediaType)
        {
            return new DefaultRepresentation(
                new Transitions(new AspNetMvcUrlGenerator()), 
                new AcceptHeaderToSerializer().For(mediaType));
        }
    }
}