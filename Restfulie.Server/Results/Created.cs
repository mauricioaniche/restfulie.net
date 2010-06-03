using System.Net;
using System.Web.Mvc;
using Restfulie.Server.Results.Decorators;

namespace Restfulie.Server.Results
{
    public class Created : RestfulieResult
    {
        private readonly string location;

        public Created() {}
        public Created(IBehaveAsResource resource, string location) : base(resource)
        {
            this.location = location;
        }
        public Created(IBehaveAsResource resource) : base(resource) {}

        public Created(string location)
        {
            this.location = location;
        }


        public override void ExecuteResult(ControllerContext context)
        {
            var decorators = new StatusCode((int) HttpStatusCode.Created,
                             new Location(location,
                             new Content(BuildContent())));

            Execute(context, decorators);
        }
    }
}
