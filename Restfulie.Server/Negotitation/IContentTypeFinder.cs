using System.Web.Mvc;

namespace Restfulie.Server.Negotitation
{
    public interface IContentTypeFinder
    {
        string FindIn(ControllerContext context);
    }
}
