using System;
using System.Web.Mvc;

namespace Restfulie.Server.Negotitation
{
    public class DefaultRequestInfoFinder : IRequestInfoFinder
    {
        public string GetAcceptHeaderIn(ControllerContext context)
        {
            return context.RequestContext.HttpContext.Request.Headers["accept"]; 
        }

        public string GetContentTypeIn(ControllerContext context)
        {
            return context.RequestContext.HttpContext.Request.ContentType;
        }

        public string GetContent(ControllerContext context)
        {
            throw new NotImplementedException();
        }
    }
}
