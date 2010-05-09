namespace Restfulie.Server
{
    public interface IUrlGenerator
    {
        string For(string action, string controller);
    }
}
