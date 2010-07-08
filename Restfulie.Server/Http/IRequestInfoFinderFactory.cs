using System.Web;

namespace Restfulie.Server.Http
{
    public interface IRequestInfoFinderFactory
    {
        IRequestInfoFinder BasedOn(HttpContextBase httpContext);
    }
}