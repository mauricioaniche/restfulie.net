using System;
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

        public override sealed void ExecuteResult(ControllerContext context)
        {
            var responseInfo = new ResponseInfo
                                   {
                                       Location = Location,
                                       StatusCode = StatusCode
                                   };

            if (resource != null)
            {
                Marshaller.Build(context, resource, responseInfo);
            }
            else if (resources != null)
            {
                Marshaller.Build(context, resources, responseInfo);
            }
            else
            {
                Marshaller.Build(context, responseInfo);
            }
        }
    }
}
