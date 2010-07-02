using System.Web;

namespace Restfulie.Server.Request
{
    public interface IRequestInfoFinderFactory
    {
        IRequestInfoFinder BasedOn(HttpContextBase httpContext);
    }
}