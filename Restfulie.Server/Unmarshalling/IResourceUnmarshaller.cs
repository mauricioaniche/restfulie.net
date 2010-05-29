namespace Restfulie.Server.Unmarshalling
{
    public interface IResourceUnmarshaller
    {
        T ToResource<T>(string xml) where T : IBehaveAsResource;
    }
}
