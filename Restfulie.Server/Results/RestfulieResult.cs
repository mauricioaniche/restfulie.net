using System.Collections.Generic;
using System.Web.Mvc;
using Restfulie.Server.MediaTypes;
using Restfulie.Server.Results.Decorators.Holders;

namespace Restfulie.Server.Results
{
    public abstract class RestfulieResult : ActionResult
    {
        private readonly IEnumerable<IBehaveAsResource> resources;
        private readonly IBehaveAsResource resource;

        public IMediaType MediaType { get; set; }
        public IResultDecoratorHolder DecoratorHolder { get; set; }

        protected RestfulieResult()
        {
        }

        protected RestfulieResult(IBehaveAsResource resource)
        {
            this.resource = resource;
        }

        protected RestfulieResult(IEnumerable<IBehaveAsResource> resources)
        {
            this.resources = resources;
        }

        protected object GetPassedResource()
        {
            return (object) resource ?? resources;
        }

        protected string BuildContent()
        {
            if (resource != null)
            {
                return MediaType.Marshaller.Build(resource);
            }

            if (resources != null)
            {
                return MediaType.Marshaller.Build(resources);
            }

            return string.Empty;
        }
    }
}
