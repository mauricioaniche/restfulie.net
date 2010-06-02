using System.Collections.Generic;
using System.Web.Mvc;
using Restfulie.Server.Marshalling;

namespace Restfulie.Server.Results
{
    public abstract class RestfulieResult : ActionResult
    {
        private readonly IEnumerable<IBehaveAsResource> resources;
        private readonly IBehaveAsResource resource;
        protected string Location;
        public abstract int StatusCode { get; }
        public IResourceMarshaller Marshaller { get; set; }

        protected RestfulieResult() { }

        protected RestfulieResult(IBehaveAsResource resource)
        {
            this.resource = resource;
        }

        protected RestfulieResult(IEnumerable<IBehaveAsResource> resources)
        {
            this.resources = resources;
        }

        public override void ExecuteResult(ControllerContext context)
        {
            if (HasResource())
            {
                Marshaller.Build(context, resource, GetResponseInfo());
            }
            else if (HasListOfResources())
            {
                Marshaller.Build(context, resources, GetResponseInfo());
            }
            else
            {
                Marshaller.Build(context, GetResponseInfo());
            }
        }

        private ResponseInfo GetResponseInfo()
        {
            return new ResponseInfo
                       {
                           Location = Location,
                           StatusCode = StatusCode
                       };
        }

        private bool HasListOfResources()
        {
            return resources != null;
        }

        private bool HasResource()
        {
            return resource != null;
        }
    }
}
