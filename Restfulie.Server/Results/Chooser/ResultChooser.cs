using System.Web.Mvc;
using Restfulie.Server.Extensions;
using Restfulie.Server.Http;
using Restfulie.Server.MediaTypes;

namespace Restfulie.Server.Results.Chooser
{
    public class ResultChooser : IResultChooser
    {
        public ActionResult BasedOnMediaType(ActionExecutedContext context, IMediaType type, IRequestInfoFinder requestInfoFinder)
        {
            if (!context.Result.IsRestfulieResult()) return context.Result;
            return (type is HTML) ? AspNetResult(context) : RestfulieResult(context, type, requestInfoFinder);
        }

        private ActionResult RestfulieResult(ActionExecutedContext context, IMediaType type, IRequestInfoFinder requestInfoFinder)
        {
            var result = (RestfulieResult)context.Result;
            result.MediaType = type;
            result.RequestInfo = requestInfoFinder;

            return result; 
        }

        private ActionResult AspNetResult(ActionExecutedContext context)
        {
            var result = (RestfulieResult)context.Result;

            var viewResult = new ViewResult
                                 {
                                     TempData = context.Controller.TempData,
                                     ViewData = context.Controller.ViewData
                                 };

            viewResult.ViewData.Model = result.Model;

            return viewResult;
        }
    }
}