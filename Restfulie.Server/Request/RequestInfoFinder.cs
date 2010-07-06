using System.IO;
using System.Web;
using System.Web.Mvc;

namespace Restfulie.Server.Request
{
    public class RequestInfoFinder : IRequestInfoFinder
    {
        private readonly HttpContextBase httpContext;

        public RequestInfoFinder(HttpContextBase httpContext)
        {
            this.httpContext = httpContext;
        }

        public string GetAcceptHeader()
        {
            return httpContext.Request.Headers["accept"]; 
        }

        public string GetContentType()
        {
            return httpContext.Request.ContentType;
        }

        public string GetUrl()
        {
            return httpContext.Request.Url.AbsoluteUri;
        }

        public string GetContent()
        {
            return new StreamReader(httpContext.Request.InputStream).ReadToEnd();
        }
    }
}