using System.Web;

namespace Restfulie.Server.Http
{
    public class RequestInfoFinderFactory : IRequestInfoFinderFactory
    {
        #region IRequestInfoFinderFactory Members

        public IRequestInfoFinder BasedOn(HttpContextBase httpContext)
        {
            return new RequestInfoFinder(httpContext);
        }

        #endregion
    }
}