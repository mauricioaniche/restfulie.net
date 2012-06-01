using System.Net;
using Restfulie.Server.Results.Decorators;

namespace Restfulie.Server.Results
{
    public class NotAcceptable : RestfulieResult
    {
        public override ResultDecorator GetDecorators()
        {
            return new StatusCode((int) HttpStatusCode.NotAcceptable);
        }
    }
}