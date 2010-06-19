using System;

namespace Restfulie.Server.Unmarshalling
{
    public class NoUnmarshaller : IResourceUnmarshaller
    {
        public object Build(string xml, Type objectType)
        {
            return null;
        }
    }
}
