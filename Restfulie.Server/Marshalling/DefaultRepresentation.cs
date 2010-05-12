using Restfulie.Server.Marshalling.Serializers;

namespace Restfulie.Server.Marshalling
{
    public class DefaultRepresentation : IRepresentationBuilder
    {
        private readonly Transitions transitions;
        private readonly ISerializer serializer;

        public DefaultRepresentation(Transitions transitions, ISerializer serializer)
        {
            this.transitions = transitions;
            this.serializer = serializer;
        }

        public string Build(IBehaveAsResource resource)
        {
            resource.Transitions(transitions);

            return serializer.Serialize(resource, transitions.All);
        }
    }
}