using Restfulie.Server.ResourceRepresentation.Serializers;
using Restfulie.Server.ResourceRepresentation.UrlGenerators;

namespace Restfulie.Server.ResourceRepresentation
{
    public class XMLAndHypermediaRepresentation : IRepresentationBuilder
    {
        private readonly IUrlGenerator urlGenerator;
        private readonly ISerializer serializer;

        public XMLAndHypermediaRepresentation(IUrlGenerator urlGenerator, ISerializer serializer)
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
