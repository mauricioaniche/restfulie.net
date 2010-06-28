using Restfulie.Server.Marshalling.UrlGenerators;

namespace Restfulie.Server
{
    public class RelationsFactory : IRelationsFactory
    {
        private readonly IUrlGenerator urlGenerator;

        public RelationsFactory(IUrlGenerator urlGenerator)
        {
            this.urlGenerator = urlGenerator;
        }

        public Relations NewRelations()
        {
            return new Relations(urlGenerator);
        }
    }
}
