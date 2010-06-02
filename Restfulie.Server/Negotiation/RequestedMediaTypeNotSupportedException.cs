using System;

namespace Restfulie.Server.Negotiation
{
    public class RequestedMediaTypeNotSupportedException : Exception
    {
        public RequestedMediaTypeNotSupportedException() : base("Media type not supported.") { }
    }
}