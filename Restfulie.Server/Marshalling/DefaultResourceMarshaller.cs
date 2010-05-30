using System.Collections.Generic;
using Restfulie.Server.Marshalling.Serializers;
using System.Linq;

namespace Restfulie.Server.Marshalling
{
    public class DefaultResourceMarshaller : IResourceMarshaller
    {
        private readonly Relations relations;
        private readonly IResourceSerializer serializer;

        public DefaultResourceMarshaller(Relations relations, IResourceSerializer serializer)
        {
            this.relations = relations;
            this.serializer = serializer;
        }

        public string Build(IBehaveAsResource resource)
        {
            var all = resource.GetRelations(relations);

            return serializer.Serialize(resource, all);
        }

        public string Build(IEnumerable<IBehaveAsResource> resources)
        {
            var listOfResources = new Dictionary<IBehaveAsResource, IList<Relation>>();
            foreach(var resource in resources)
            {
                var allRelations = resource.GetRelations(relations);
                listOfResources.Add(resource, allRelations);
            }

            return serializer.Serialize(listOfResources);
        }

        public string MediaType
        {
            get { return serializer.Format; }
        }
    }
}