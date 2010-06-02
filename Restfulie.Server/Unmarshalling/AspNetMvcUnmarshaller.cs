using System;

namespace Restfulie.Server.Unmarshalling
{
    public class AspNetMvcUnmarshaller : IResourceUnmarshaller
    {
        public IBehaveAsResource ToResource(string xml, Type objectType)
        {
            return null;
        }
    }
}
