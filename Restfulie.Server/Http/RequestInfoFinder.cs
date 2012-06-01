using System.IO;
using System.Web;

namespace Restfulie.Server.Http
{
    public class RequestInfoFinder : IRequestInfoFinder
    {
        private readonly HttpContextBase httpContext;

        public RequestInfoFinder(HttpContextBase httpContext)
        {
            this.httpContext = httpContext;
        }

        #region IRequestInfoFinder Members

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

        #endregion
    }
}