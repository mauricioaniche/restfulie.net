namespace Restfulie.Server.Marshalling
{
    public interface IRepresentationBuilder
    {
        string Build(IBehaveAsResource resource);
    }
}