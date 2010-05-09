namespace Restfulie.Server.UrlGenerators
{
    public interface IUrlGenerator
    {
        string For(string action, string controller);
    }
}