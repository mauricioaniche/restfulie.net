using System.Collections.Generic;
using Restfulie.Server.Marshalling.Serializers;

namespace Restfulie.Server.Marshalling
{
    public class RestfulieMarshaller : IResourceMarshaller
    {
        private readonly Relations relations;
        private readonly IResourceSerializer serializer;

        public RestfulieMarshaller(Relations relations, IResourceSerializer serializer)
        {
            this.relations = relations;
            this.serializer = serializer;
        }

        public string Build(IBehaveAsResource resource)
        {
            var all = resource.GetRelations(relations);
            var content = serializer.Serialize(resource, all);
            return content;
        }

        public string Build(IEnumerable<IBehaveAsResource> resources)
        {
            var listOfResources = new Dictionary<IBehaveAsResource, IList<Relation>>();
            foreach (var resource in resources)
            {
                var allRelations = resource.GetRelations(relations);
                listOfResources.Add(resource, allRelations);
            }

            var content = serializer.Serialize(listOfResources);

            return content;
        }
    }
}