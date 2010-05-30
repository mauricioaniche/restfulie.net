using System.Collections.Generic;

namespace Restfulie.Server.Marshalling
{
    public interface IResourceMarshaller
    {
        string Build(IBehaveAsResource resource);
        string Build(IEnumerable<IBehaveAsResource> resources);
    }
}