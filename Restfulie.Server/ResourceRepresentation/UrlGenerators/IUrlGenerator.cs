namespace Restfulie.Server.ResourceRepresentation.UrlGenerators
{
    public interface IUrlGenerator
    {
        string For(string controller, string action);
    }
}