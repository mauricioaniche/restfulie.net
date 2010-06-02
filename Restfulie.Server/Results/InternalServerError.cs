using System;

namespace Restfulie.Server.Results
{
    public class InternalServerError : RestfulieResult
    {
        public override int StatusCode
        {
            get { return (int) StatusCodes.InternalServerError; }
        }
    }
}
