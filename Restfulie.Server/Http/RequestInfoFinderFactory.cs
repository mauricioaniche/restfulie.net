using System.Web;

namespace Restfulie.Server.Http
{
    public class RequestInfoFinderFactory : IRequestInfoFinderFactory
    {
        public IRequestInfoFinder BasedOn(HttpContextBase httpContext)
        {
            return new RequestInfoFinder(httpContext);
        }
    }
}