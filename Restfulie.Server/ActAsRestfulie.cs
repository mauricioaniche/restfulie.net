using System.Web.Mvc;
using Restfulie.Server.Marshalling;
using Restfulie.Server.Results;

namespace Restfulie.Server
{
    public class ActAsRestfulie : ActionFilterAttribute
    {
        private IRepresentationBuilder builder;

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            builder = new RepresentationFactory().BasedOnMediaType(InAcceptHeader(filterContext));

            base.OnActionExecuting(filterContext);
        }

        public override void OnResultExecuting(ResultExecutingContext filterContext)
        {
            var result = (RestfulieResult)filterContext.Result;
            result.RepresentationBuilder = builder;

            base.OnResultExecuting(filterContext);
        }

        private string InAcceptHeader(ControllerContext filterContext)
        {
            return filterContext.RequestContext.HttpContext.Request.Headers["accept"];
        }
    }
}
