namespace Restfulie.Server.Marshalling
{
    public interface IMarshallerFactory
    {
        IResourceMarshaller BasedOnMediaType(string mediaType);
    }
}