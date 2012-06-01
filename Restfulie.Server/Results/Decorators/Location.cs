using System.Web.Mvc;

namespace Restfulie.Server.Results.Decorators
{
    public class Location : ResultDecorator
    {
        private readonly string location;

        public Location(string location)
        {
            this.location = location;
        }

        public Location(string location, ResultDecorator nextDecorator) : base(nextDecorator)
        {
            this.location = location;
        }

        public override void Execute(ControllerContext context)
        {
            if (!string.IsNullOrEmpty(location))
                context.HttpContext.Response.RedirectLocation = location;

            Next(context);
        }
    }
}