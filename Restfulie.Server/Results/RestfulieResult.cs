using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Restfulie.Server.Marshalling;

namespace Restfulie.Server.Results
{
    public abstract class RestfulieResult : ActionResult
    {
        protected string Location;
        public abstract int StatusCode { get; }
        public IResourceMarshaller Marshaller { get; set; }
        private Action<ControllerContext> startMarshalling;  

        protected RestfulieResult()
        {
            startMarshalling = (context) => Marshaller.Build(context, GetResponseInfo());
        }

        protected RestfulieResult(IBehaveAsResource resource)
        {
            startMarshalling = (context) => Marshaller.Build(context, resource, GetResponseInfo());
        }

        protected RestfulieResult(IEnumerable<IBehaveAsResource> resources)
        {
            startMarshalling = (context) => Marshaller.Build(context, resources, GetResponseInfo());
        }

        public override void ExecuteResult(ControllerContext context)
        {
            startMarshalling(context);
        }

        private ResponseInfo GetResponseInfo()
        {
            return new ResponseInfo
                       {
                           Location = Location,
                           StatusCode = StatusCode
                       };
        }
    }
}
