using Restfulie.Server.Marshalling;

namespace Restfulie.Server
{
    public interface IRepresentationFactory
    {
        IRepresentationBuilder BasedOnMediaType(string mediaType);
    }
}
