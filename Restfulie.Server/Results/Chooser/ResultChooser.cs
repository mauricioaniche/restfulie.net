using System.Web.Mvc;
using Restfulie.Server.Extensions;
using Restfulie.Server.MediaTypes;

namespace Restfulie.Server.Results.Chooser
{
    public class ResultChooser : IResultChooser
    {
        public ActionResult BasedOnMediaType(ActionExecutedContext context, IMediaType type)
        {
            if (!context.Result.IsRestfulieResult()) return context.Result;

            return (type is HTML) ? AspNetResult(context) : RestfulieResult(context, type);
        }

        private ActionResult RestfulieResult(ActionExecutedContext context, IMediaType type)
        {
            var result = (RestfulieResult)context.Result;
            result.MediaType = type;

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