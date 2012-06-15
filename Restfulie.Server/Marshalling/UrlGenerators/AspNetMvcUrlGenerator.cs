using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Restfulie.Server.Marshalling.UrlGenerators
{
    public class AspNetMvcUrlGenerator : IUrlGenerator
    {
        #region IUrlGenerator Members

        public string For(string controller, string action, IDictionary<string, object> values)
        {
            var httpContextWrapper = new HttpContextWrapper(HttpContext.Current);
            var urlHelper = new UrlHelper(new RequestContext(httpContextWrapper, RouteTable.Routes.GetRouteData(httpContextWrapper)));

            return FullApplicationPath(httpContextWrapper.Request) + urlHelper.Action(action, controller, new RouteValueDictionary(values));
        }

        #endregion

        private string FullApplicationPath(HttpRequestBase request)
        {
            return request.Url.Scheme + "://" + request.Url.Authority;
        }
    }
}