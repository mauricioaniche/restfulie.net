using System;

namespace Restfulie.Server.Exceptions
{
    public class MediaTypeNotSupportedException : Exception
    {
        public MediaTypeNotSupportedException() : base("Media type not supported.") { }
    }
}
