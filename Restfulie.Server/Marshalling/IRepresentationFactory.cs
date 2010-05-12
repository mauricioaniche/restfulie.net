namespace Restfulie.Server.Marshalling
{
    public interface IRepresentationFactory
    {
        IResourceRepresentation BasedOnMediaType(string mediaType);
    }
}