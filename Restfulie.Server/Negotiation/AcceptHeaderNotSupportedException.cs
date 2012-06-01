using System;

namespace Restfulie.Server.Negotiation
{
    public class AcceptHeaderNotSupportedException : Exception
    {
        public AcceptHeaderNotSupportedException() : base("Media type not supported.") {}
    }
}