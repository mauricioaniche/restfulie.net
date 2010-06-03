using System.Web.Mvc;

namespace Restfulie.Server.Results.Decorators
{
    public class StatusCode : ResultDecorator
    {
        private readonly int statusCode;

        public StatusCode(int statusCode)
        {
            this.statusCode = statusCode;
        }

        public StatusCode(int statusCode, ResultDecorator nextDecorator) : base(nextDecorator)
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