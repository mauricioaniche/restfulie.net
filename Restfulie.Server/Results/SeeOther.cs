using System.Net;

namespace Restfulie.Server.Results
{
    public class SeeOther : RestfulieResult
    {
        public SeeOther(string location)
        {
            Location = location;
        }

        public override int StatusCode
        {
            get { return (int) HttpStatusCode.SeeOther; }
        }
    }
}
