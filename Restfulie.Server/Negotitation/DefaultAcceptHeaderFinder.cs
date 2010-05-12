using System.Web.Mvc;

namespace Restfulie.Server.Negotitation
{
    public class DefaultAcceptHeaderFinder : IAcceptHeaderFinder
    {
        public string FindIn(ControllerContext context)
        {
            return context.RequestContext.HttpContext.Request.Headers["accept"]; 
        }
    }
}
