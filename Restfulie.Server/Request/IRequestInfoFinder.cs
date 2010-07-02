using System.Web.Mvc;

namespace Restfulie.Server.Request
{
    public interface IRequestInfoFinder
    {
        string GetAcceptHeaderIn(ControllerContext context);
        string GetContentTypeIn(ControllerContext context);
        string GetContent(ControllerContext context);
    }
}