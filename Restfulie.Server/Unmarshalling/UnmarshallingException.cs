using System;

namespace Restfulie.Server.Unmarshalling
{
    public class UnmarshallingException : Exception
    {
        public UnmarshallingException(string message) : base(message) {}
    }
}