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