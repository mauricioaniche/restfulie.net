using Restfulie.Server.Request;

namespace Restfulie.Server.Marshalling
{
    public interface IResourceMarshaller
    {
        string Build(object model, IRequestInfoFinder finder);
    }
}