using Restfulie.Server.Marshalling.Serializers;
using Restfulie.Server.Marshalling.UrlGenerators;

namespace Restfulie.Server.Marshalling
{
    public class RepresentationFactory
    {
        public IRepresentationBuilder GetDefault()
        {
            return new DefaultRepresentation(new AspNetMvcUrlGenerator(), new XmlAndHypermediaSerializer());
        }

        public IRepresentationBuilder GetDefault(ISerializer serializer)
        {
            return new DefaultRepresentation(new AspNetMvcUrlGenerator(), serializer);
        }
    }
}