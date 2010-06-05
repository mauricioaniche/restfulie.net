namespace Restfulie.Server.Marshalling
{
    public interface IResourceMarshaller
    {
        string Build(object resource);
    }
}