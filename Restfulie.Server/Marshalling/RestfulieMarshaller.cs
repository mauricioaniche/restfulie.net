using System.Collections.Generic;
using Restfulie.Server.Extensions;
using Restfulie.Server.Http;
using Restfulie.Server.Marshalling.Serializers;

namespace Restfulie.Server.Marshalling
{
    public class RestfulieMarshaller : IResourceMarshaller
    {
        private readonly IHypermediaInjector hypermedia;
        private readonly IRelationsFactory relationsFactory;
        private readonly IResourceSerializer serializer;

        public RestfulieMarshaller(IRelationsFactory relationsFactory, IResourceSerializer serializer, IHypermediaInjector hypermedia)
        {
            this.relationsFactory = relationsFactory;
            this.serializer = serializer;
            this.hypermedia = hypermedia;
        }

        #region IResourceMarshaller Members

        public string Build(object model, IRequestInfoFinder finder)
        {
            var content = serializer.Serialize(model);

            if (model.GetType().IsAResource())
            {
                var resource = model.AsResource();
                var relations = relationsFactory.NewRelations();
                resource.SetRelations(relations);
                content = hypermedia.Inject(content, relations, finder);
            } else if (model.GetType().IsAListOfResources())
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

        #endregion
    }
}