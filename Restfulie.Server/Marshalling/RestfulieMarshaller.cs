using System.Collections.Generic;
using Restfulie.Server.Marshalling.Serializers;
using Restfulie.Server.Extensions;

namespace Restfulie.Server.Marshalling
{
    public class RestfulieMarshaller : IResourceMarshaller
    {
        private readonly IRelationsFactory relationsFactory;
        private readonly IResourceSerializer serializer;
        private readonly IHypermediaInserter hypermedia;

        public RestfulieMarshaller(IRelationsFactory relationsFactory, IResourceSerializer serializer, IHypermediaInserter hypermedia)
        {
            this.relationsFactory = relationsFactory;
            this.serializer = serializer;
            this.hypermedia = hypermedia;
        }

        public string Build(object model)
        {
            var content = serializer.Serialize(model);

            if(model.GetType().IsAResource())
            {
                var relations = relationsFactory.NewRelations();
                ((IBehaveAsResource) model).SetRelations(relations);
                content = hypermedia.Insert(content, relations);
            }

            else if(model.GetType().IsAListOfResources())
            {
                var allRelations = new List<Relations>();

                var resources = model.AsResourceArray();
                foreach (var resource in resources)
                {
                    var relations = relationsFactory.NewRelations();
                    resource.SetRelations(relations);
                    allRelations.Add(relations);
                }

                content = hypermedia.Insert(content, allRelations);
            }

            return content;
        }
    }
}