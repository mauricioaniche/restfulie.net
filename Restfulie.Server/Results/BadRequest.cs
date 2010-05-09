using System.Web.Mvc;

namespace Restfulie.Server.Results
{
    public class BadRequest : ActionResult
    {
        private readonly string message;

        public BadRequest()
        {
        }

        public BadRequest(string message) : this()
        {
            this.message = message;
        }

        public override void ExecuteResult(ControllerContext context)
        {
            context.HttpContext.Response.StatusCode = (int)StatusCodes.BadRequest;

            if(MessageWasPassed())
            {
                WriteResource(context);
            }
        }

        private bool MessageWasPassed()
        {
            return !string.IsNullOrEmpty(message);
        }

        private void WriteResource(ControllerContext context)
        {
            context.HttpContext.Response.Output.Write(message);
            context.HttpContext.Response.Output.Flush();
        }
    }
}