using System.Web.Mvc;
using Restfulie.Server.Marshalling;

namespace Restfulie.Server.Results
{
    public abstract class RestfulieResult : ActionResult
    {
        public IResourceMarshaller Marshaller { get; set; }

        protected void SetStatusCode(ControllerContext context, StatusCodes status)
        {
            context.HttpContext.Response.StatusCode = (int)status;
        }

        protected void SetContentType(ControllerContext context, string type)
        {
            context.HttpContext.Response.ContentType = type;
        }

        protected void Write(ControllerContext context, string content)
        {
            context.HttpContext.Response.Output.Write(content);
            context.HttpContext.Response.Output.Flush();
        }
    }
}
