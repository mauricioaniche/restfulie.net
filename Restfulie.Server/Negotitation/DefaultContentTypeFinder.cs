using System.Web.Mvc;

namespace Restfulie.Server.Negotitation
{
    public class DefaultContentTypeFinder : IContentTypeFinder
    {
        public string FindIn(ControllerContext context)
        {
            return context.RequestContext.HttpContext.Request.Headers["content-type"];
        }
    }
}
