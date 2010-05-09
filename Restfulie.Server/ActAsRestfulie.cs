using System;
using System.Web.Mvc;
using System.Web.Routing;

namespace Restfulie.Server
{
    public class ActAsRestfulie : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {   
           
        }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            filterContext.Result = new Teste();
        }

        public override void OnResultExecuting(ResultExecutingContext filterContext)
        {
            
        }

        public override void OnResultExecuted(ResultExecutedContext filterContext)
        {

        }

        public class Teste : ActionResult
        {
            public override void ExecuteResult(ControllerContext context)
            {
                context.HttpContext.Response.StatusCode = 200;
            }
        }
    }
}
