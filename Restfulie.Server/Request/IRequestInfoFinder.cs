using System.Web.Mvc;

namespace Restfulie.Server.Request
{
    public interface IRequestInfoFinder
    {
        string GetAcceptHeader();
        string GetContentType();
        string GetContent();
    }
}