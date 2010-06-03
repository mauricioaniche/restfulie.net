using System.Collections.Generic;

namespace Restfulie.Server.Marshalling
{
    public class NoMarshaller : IResourceMarshaller
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
