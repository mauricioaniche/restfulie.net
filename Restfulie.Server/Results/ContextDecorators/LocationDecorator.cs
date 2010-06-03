using System.Web.Mvc;

namespace Restfulie.Server.Results.ContextDecorators
{
    public class LocationDecorator : ContextDecorator
    {
        private readonly string location;

        public LocationDecorator(string location)
        {
            this.location = location;
        }

        public LocationDecorator(string location, ContextDecorator nextDecorator): base(nextDecorator)
        {
            this.location = location;
        }

        public override void Execute(ControllerContext context)
        {
            context.HttpContext.Response.RedirectLocation = location;
            Next(context);
        }
    }
}
