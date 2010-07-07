using System.Linq;
using System.Net;
using Restfulie.Server.Results.Decorators;

namespace Restfulie.Server.Results
{
    public class Created : RestfulieResult
    {
        private readonly string location;

        public Created() {}
        public Created(object model, string location) : base(model)
        {
            this.location = location;
        }
        public Created(object model) : base(model) {}

        public Created(string location)
        {
            this.location = location;
        }

        public override ResultDecorator GetDecorators()
        {
            return new StatusCode((int)HttpStatusCode.Created,
                   new Location(location,
                   new ContentType(MediaType.Synonyms.First(),
                   new Content(BuildContent()))));
        }
    }
}
