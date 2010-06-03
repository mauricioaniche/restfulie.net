using System.Net;

namespace Restfulie.Server.Results
{
    public class NotAcceptable : RestfulieResult
    {
        public override int StatusCode
        {
            get { return (int) HttpStatusCode.NotAcceptable; }
        }
    }
}
