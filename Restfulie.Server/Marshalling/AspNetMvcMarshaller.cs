using System.Collections.Generic;

namespace Restfulie.Server.Marshalling
{
    // used when html is the media type and we need to let asp.net mvc work!
    public class AspNetMvcMarshaller : IResourceMarshaller
    {
        public string Build(IBehaveAsResource resource)
        {
            return string.Empty;
        }

        public string Build(IEnumerable<IBehaveAsResource> resources)
        {
            return string.Empty;
        }
    }
}
