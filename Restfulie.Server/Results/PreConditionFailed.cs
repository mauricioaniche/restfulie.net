using System.Net;

namespace Restfulie.Server.Results
{
    public class PreconditionFailed : RestfulieResult
    {
        public override int StatusCode
        {
            get { return (int) HttpStatusCode.PreconditionFailed; }
        }
    }
}
