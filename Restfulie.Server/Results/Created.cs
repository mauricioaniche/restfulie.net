using System.Net;
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

        public override ResultDecorator GetDecorators()
        {
            return new StatusCode((int)HttpStatusCode.Created,
                   new Location(location,
                   new Content(BuildContent())));
        }
    }
}
