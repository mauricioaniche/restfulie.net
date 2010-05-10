using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Restfulie.Server.ResourceRepresentation.UrlGenerators
{
    public class AspNetMvcUrlGenerator : IUrlGenerator
    {
        public string For(string controller, string action)
        {
            var httpContextWrapper = new HttpContextWrapper(HttpContext.Current);
            var urlHelper = new UrlHelper(new RequestContext(httpContextWrapper, RouteTable.Routes.GetRouteData(httpContextWrapper)));

            return urlHelper.Action(action, controller);
        }
    }
}