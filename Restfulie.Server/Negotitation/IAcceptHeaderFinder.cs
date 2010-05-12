using System.Web.Mvc;

namespace Restfulie.Server.Negotitation
{
    public interface IAcceptHeaderFinder
    {
        string FindIn(ControllerContext context);
    }
}
