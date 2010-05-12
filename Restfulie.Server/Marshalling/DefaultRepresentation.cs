using Restfulie.Server.Marshalling.Serializers;

namespace Restfulie.Server.Marshalling
{
    public class DefaultRepresentation : IResourceRepresentation
    {
        private readonly Transitions transitions;
        private readonly IResourceSerializer serializer;

        public DefaultRepresentation(Transitions transitions, IResourceSerializer serializer)
        {
            this.transitions = transitions;
            this.serializer = serializer;
        }

        public string Build(IBehaveAsResource resource)
        {
            resource.SetTransitions(transitions);

            return serializer.Serialize(resource, transitions.All);
        }
    }
}