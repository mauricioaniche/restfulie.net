using System.Net;
using System.Web.Mvc;
using Restfulie.Server.Results.ContextDecorators;

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
            var decorators = new StatusCodeDecorator((int) HttpStatusCode.Created,
                             new LocationDecorator(location,
                             new ContentDecorator(BuildContent())));

            DecoratorHolder.Decorate(context, decorators, GetPassedResource());
        }
    }
}
