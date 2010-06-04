using System;

namespace Restfulie.Server.Unmarshalling
{
    public class AspNetMvcUnmarshaller : IResourceUnmarshaller
    {
        public IBehaveAsResource ToResource(string xml, Type objectType)
        {
            return null;
        }

        public IBehaveAsResource[] ToListOfResources(string xml, Type objectType)
        {
            return null;
        }
    }
}
