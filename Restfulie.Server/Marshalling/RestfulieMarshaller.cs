using System.Collections.Generic;
using Restfulie.Server.Marshalling.Serializers;
using Restfulie.Server.Extensions;
using Restfulie.Server.Request;

namespace Restfulie.Server.Marshalling
{
    public class RestfulieMarshaller : IResourceMarshaller
    {
        private readonly IRelationsFactory relationsFactory;
        private readonly IResourceSerializer serializer;
        private readonly IHypermediaInjector hypermedia;

        public RestfulieMarshaller(IRelationsFactory relationsFactory, IResourceSerializer serializer, IHypermediaInjector hypermedia)
        {
            this.relationsFactory = relationsFactory;
            this.serializer = serializer;
            this.hypermedia = hypermedia;
        }

        public string Build(object model, IRequestInfoFinder finder)
        {
            var content = serializer.Serialize(model);

            if(model.GetType().IsAResource())
            {
                var relations = relationsFactory.NewRelations();
                ((IBehaveAsResource) model).SetRelations(relations);
                content = hypermedia.Inject(content, relations, finder);
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

                content = hypermedia.Inject(content, allRelations, finder);
            }

            return content;
        }
    }
}