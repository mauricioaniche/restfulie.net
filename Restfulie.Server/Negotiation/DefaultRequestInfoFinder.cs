using System;
using System.IO;
using System.Web.Mvc;

namespace Restfulie.Server.Negotiation
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
            return new StreamReader(context.RequestContext.HttpContext.Request.InputStream).ReadToEnd();
        }
    }
}