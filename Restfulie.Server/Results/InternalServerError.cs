using System;
using System.Net;

namespace Restfulie.Server.Results
{
    public class InternalServerError : RestfulieResult
    {
        public override int StatusCode
        {
            get { return (int) HttpStatusCode.InternalServerError; }
        }
    }
}
