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
        private readonly string message;
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

        protected RestfulieResult(string message)
        {
            this.message = message;
        }

        public override sealed void ExecuteResult(ControllerContext context)
        {
            Marshaller.Build(
                context,
                new MarshallingInfo
                {
                    Location = Location,
                    Message = message,
                    Resource = resource,
                    Resources = resources,
                    StatusCode = StatusCode
                });
        }
    }
}
