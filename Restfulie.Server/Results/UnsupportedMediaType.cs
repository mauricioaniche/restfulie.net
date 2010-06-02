using System;

namespace Restfulie.Server.Results
{
    public class UnsupportedMediaType : RestfulieResult
    {
        public override int StatusCode
        {
            get { return (int) StatusCodes.UnsupportedMediaType; }
        }
    }
}
