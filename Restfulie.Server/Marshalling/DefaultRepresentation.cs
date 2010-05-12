using Restfulie.Server.Marshalling.Serializers;
using Restfulie.Server.Marshalling.UrlGenerators;

namespace Restfulie.Server.Marshalling
{
    public class DefaultRepresentation : IRepresentationBuilder
    {
        private readonly IUrlGenerator urlGenerator;
        private readonly ISerializer serializer;

        public DefaultRepresentation(IUrlGenerator urlGenerator, ISerializer serializer)
        {
            this.urlGenerator = urlGenerator;
            this.serializer = serializer;
        }

        public string Build(IBehaveAsResource resource)
        {
            var transitions = new Transitions(urlGenerator);
            resource.Transitions(transitions);

            return serializer.Serialize(resource, transitions.All);
        }
    }
}