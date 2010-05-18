using System.Collections.Generic;

namespace Restfulie.Server.Marshalling
{
    public interface IResourceRepresentation
    {
        string Build(IBehaveAsResource resource);
        string Build(IEnumerable<IBehaveAsResource> resources);
    }
}