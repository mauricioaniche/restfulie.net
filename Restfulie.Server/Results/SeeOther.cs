using System.Net;
using Restfulie.Server.Results.Decorators;

namespace Restfulie.Server.Results
{
    public class SeeOther : RestfulieResult
    {
        private readonly string location;

        public SeeOther(string location)
        {
            this.location = location;
        }

        public override ResultDecorator GetDecorators()
        {
            return new StatusCode((int) HttpStatusCode.SeeOther,
                                  new Location(location));
        }
    }
}