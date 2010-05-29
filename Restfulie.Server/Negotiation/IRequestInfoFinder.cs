using System.Web.Mvc;

namespace Restfulie.Server.Negotiation
{
    public interface IRequestInfoFinder
    {
        string GetAcceptHeaderIn(ControllerContext context);
        string GetContentTypeIn(ControllerContext context);
        string GetContent(ControllerContext context);
    }
}