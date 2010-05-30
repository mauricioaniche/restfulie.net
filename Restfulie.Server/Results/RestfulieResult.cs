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
        protected abstract int StatusCode { get; }
        public IResourceMarshaller Marshaller { get; set; }

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

        protected RestfulieResult(string message)
        {
            this.message = message;
        }

        public override sealed void ExecuteResult(ControllerContext context)
        {
            SetStatusCode(context);
            SetLocation(context);
            WriteContent(context);
        }

        private void SetLocation(ControllerContext context)
        {
            if (!string.IsNullOrEmpty(Location))
            {
                context.HttpContext.Response.RedirectLocation = Location;
            }
        }

        private void SetStatusCode(ControllerContext context)
        {
            context.HttpContext.Response.StatusCode = StatusCode;
        }

        private void SetContentType(ControllerContext context, string type)
        {
            context.HttpContext.Response.ContentType = type;
        }

        private void Write(ControllerContext context, string content)
        {
            context.HttpContext.Response.Output.Write(content);
            context.HttpContext.Response.Output.Flush();
        }

        private void WriteContent(ControllerContext context)
        {
            if (ResourceWasPassed())
            {
                Write(context, Marshaller.Build(resource));
                SetContentType(context, Marshaller.MediaType);
            }
            else if (ResourcesWerePassed())
            {
                Write(context, Marshaller.Build(resources));
                SetContentType(context, Marshaller.MediaType);
            }
            else if (MessageWasPassed())
            {
                Write(context, message);
            }
        }


        private bool ResourcesWerePassed()
        {
            return resources != null;
        }

        private bool ResourceWasPassed()
        {
            return resource != null;
        }

        private bool MessageWasPassed()
        {
            return !string.IsNullOrEmpty(message);
        }
    }
}
