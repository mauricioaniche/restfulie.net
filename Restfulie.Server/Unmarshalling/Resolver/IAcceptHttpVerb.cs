using System.Web.Mvc;

namespace Restfulie.Server.Unmarshalling.Resolver
{
    public interface IAcceptHttpVerb
    {
        bool IsValid(ControllerContext context);
    }
}