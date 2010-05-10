namespace Restfulie.Server.ResourceRepresentation
{
    public interface IRepresentationBuilder
    {
        string Build(IBehaveAsResource resource);
    }
}
