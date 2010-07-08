namespace Restfulie.Server.Http
{
    public interface IRequestInfoFinder
    {
        string GetAcceptHeader();
        string GetContentType();
        string GetContent();
        string GetUrl();
    }
}