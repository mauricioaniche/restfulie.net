using System;
using System.Collections.Generic;
using Restfulie.Server.Marshalling.Serializers;
using Restfulie.Server.Extensions;

namespace Restfulie.Server.Marshalling
{
    public class RestfulieMarshaller : IResourceMarshaller
    {
        private readonly Relations relations;
        private readonly IResourceSerializer serializer;
        private readonly IHypermediaInserter hypermedia;

        public RestfulieMarshaller(Relations relations, IResourceSerializer serializer, IHypermediaInserter hypermedia)
        {
            this.relations = relations;
            this.serializer = serializer;
            this.hypermedia = hypermedia;
        }

        public string Build(IBehaveAsResource resource)
        {
            //var all = resource.GetRelations(relations);
            //var content = serializer.Serialize(resource, all);
            //return content;
            return string.Empty;
        }

        public string Build(IEnumerable<IBehaveAsResource> resources)
        {
            //var listOfResources = new Dictionary<IBehaveAsResource, IList<Relation>>();
            //foreach (var resource in resources)
            //{
            //    var allRelations = resource.GetRelations(relations);
            //    listOfResources.Add(resource, allRelations);
            //}

            //var content = serializer.Serialize(listOfResources);

            //return content;
            return string.Empty;
        }

        public string Build(object model)
        {
            var content = serializer.Serialize(model);

            if(model.GetType().IsAResource())
            {
                var allRelations = ((IBehaveAsResource) model).GetRelations(relations);
                content = hypermedia.Insert(content, allRelations);
            }

            if(model.GetType().IsAListOfResources())
            {
                var allRelations = new List<IList<Relation>>();

                var resources = model.AsResourceArray();
                foreach (var resource in resources)
                {
                    allRelations.Add(resource.GetRelations(relations));
                }

                content = hypermedia.Insert(content, allRelations);
            }

            return content;
        }
    }
}