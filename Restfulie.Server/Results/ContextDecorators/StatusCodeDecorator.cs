using System.Web.Mvc;

namespace Restfulie.Server.Results.ContextDecorators
{
    public class StatusCodeDecorator : ContextDecorator
    {
        private readonly int statusCode;

        public StatusCodeDecorator(int statusCode)
        {
            this.statusCode = statusCode;
        }

        public StatusCodeDecorator(int statusCode, ContextDecorator nextDecorator) : base(nextDecorator)
        {
            this.statusCode = statusCode;
        }

        public override void Execute(ControllerContext context)
        {
            context.HttpContext.Response.StatusCode = statusCode;
            Next(context);
        }
    }
}
