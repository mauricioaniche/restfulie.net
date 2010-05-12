namespace Restfulie.Server.Marshalling
{
    public interface IResourceRepresentation
    {
        string Build(IBehaveAsResource resource);
    }
}