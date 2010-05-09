using System;
using System.Web.Mvc;
using Restfulie.Server.Serializers;

namespace Restfulie.Server.Status
{
    public class Success : ActionResult
    {
        private readonly IBehaveAsResource resource;
        private readonly ISerializer serializer;

        public Success()
        {
            serializer = new DefaultXmlSerializer();
        }

        public Success(IBehaveAsResource resource) : this()
        {
            this.resource = resource;
        }

        public Success(IBehaveAsResource resource, ISerializer serializer)
        {
            this.resource = resource;
            this.serializer = serializer;
        }

        public override void ExecuteResult(ControllerContext context)
        {
            context.HttpContext.Response.StatusCode = (int)StatusCodes.Success;

            if(ResourceWasPassed())
            {
                WriteResource(context);
            }
        }

        private bool ResourceWasPassed()
        {
            return resource!=null;
        }

        private void WriteResource(ControllerContext context)
        {
            context.HttpContext.Response.Output.Write(serializer.Serialize(resource));
            context.HttpContext.Response.Output.Flush();
        }
    }
}
