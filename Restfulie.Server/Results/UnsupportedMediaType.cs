using System.Net;

namespace Restfulie.Server.Results
{
    public class UnsupportedMediaType : RestfulieResult
    {
        public override int StatusCode
        {
            get { return (int) HttpStatusCode.UnsupportedMediaType; }
        }
    }
}
