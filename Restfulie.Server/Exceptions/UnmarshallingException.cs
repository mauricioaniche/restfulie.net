using System;

namespace Restfulie.Server.Exceptions
{
    public class UnmarshallingException : Exception
    {
        public UnmarshallingException(string message) : base(message) {}
    }
}
