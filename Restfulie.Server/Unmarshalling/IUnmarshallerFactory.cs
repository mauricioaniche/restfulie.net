namespace Restfulie.Server.Unmarshalling
{
    public interface IUnmarshallerFactory
    {
        IResourceUnmarshaller BasedOn(string contentType);
    }
}
