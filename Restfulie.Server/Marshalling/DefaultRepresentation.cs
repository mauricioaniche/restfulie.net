using System;
using System.Collections.Generic;
using Restfulie.Server.Marshalling.Serializers;

namespace Restfulie.Server.Marshalling
{
    public class DefaultRepresentation : IResourceRepresentation
    {
        private readonly Relations relations;
        private readonly IResourceSerializer serializer;

        public DefaultRepresentation(Relations relations, IResourceSerializer serializer)
        {
            this.relations = relations;
            this.serializer = serializer;
        }

        public string Build(IBehaveAsResource resource)
        {
            resource.SetRelations(relations);

            return serializer.Serialize(resource, relations.All);
        }

        public string Build(IEnumerable<IBehaveAsResource> resources)
        {
            var listOfResources = new Dictionary<IBehaveAsResource, IList<Relation>>();
            foreach(var resource in resources)
            {
                resource.SetRelations(relations);
                listOfResources.Add(resource, relations.All);
                relations.Reset();
            }

            return serializer.Serialize(listOfResources);
        }
    }
}