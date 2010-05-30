namespace Restfulie.Server.Unmarshalling
{
    public interface IUnmarshallerFactory
    {
        IResourceUnmarshaller BasedOnContentType(string contentType);
    }
}
