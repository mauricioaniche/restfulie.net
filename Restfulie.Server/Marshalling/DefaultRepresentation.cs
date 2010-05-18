using System;
using System.Collections.Generic;
using Restfulie.Server.Marshalling.Serializers;
using System.Linq;

namespace Restfulie.Server.Marshalling
{
    public class DefaultRepresentation : IResourceRepresentation
    {
        private readonly Relations relations;
        private readonly IResourceSerializer serializer;
        private readonly IInflections inflections;

        public DefaultRepresentation(Relations relations, IResourceSerializer serializer, IInflections inflections)
        {
            this.relations = relations;
            this.serializer = serializer;
            this.inflections = inflections;
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

            var rootName = inflections.Inflect(resources.First().GetType().Name);
            return serializer.Serialize(listOfResources, rootName);
        }
    }
}