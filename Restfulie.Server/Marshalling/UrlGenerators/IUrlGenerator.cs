namespace Restfulie.Server.Marshalling.UrlGenerators
{
    public interface IUrlGenerator
    {
        string For(string controller, string action);
    }
}