using System.Web;

namespace Restfulie.Server.Request
{
    public class RequestInfoFinderFactory : IRequestInfoFinderFactory
    {
        public IRequestInfoFinder BasedOn(HttpContextBase httpContext)
        {
            return new RequestInfoFinder(httpContext);
        }
    }
}
