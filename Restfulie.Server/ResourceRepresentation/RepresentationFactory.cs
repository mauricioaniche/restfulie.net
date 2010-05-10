using Restfulie.Server.ResourceRepresentation.Serializers;
using Restfulie.Server.ResourceRepresentation.UrlGenerators;

namespace Restfulie.Server.ResourceRepresentation
{
    public class RepresentationFactory
    {
        public IRepresentationBuilder GetDefault()
        {
            return new XMLAndHypermediaRepresentation(new AspNetMvcUrlGenerator(), new DefaultXmlSerializer());
        }
    }
}
