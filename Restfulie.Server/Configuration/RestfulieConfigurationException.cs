using System;

namespace Restfulie.Server.Configuration
{
    public class RestfulieConfigurationException : Exception
    {
        public RestfulieConfigurationException(string message) : base(message) {}
    }
}