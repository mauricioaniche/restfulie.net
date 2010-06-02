using System;

namespace Restfulie.Server.Exceptions
{
    public class RequestedMediaTypeNotSupportedException : Exception
    {
        public RequestedMediaTypeNotSupportedException() : base("Media type not supported.") { }
    }
}
