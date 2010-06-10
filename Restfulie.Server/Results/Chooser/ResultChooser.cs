using System.Web.Mvc;
using Restfulie.Server.MediaTypes;

namespace Restfulie.Server.Results.Chooser
{
    public class ResultChooser : IResultChooser
    {
        public ActionResult Between(ActionExecutedContext context, IMediaType type)
        {
            return (type is HTML) ? AspNetResult(context) : DefaultResult(context, type);
        }

        private ActionResult DefaultResult(ActionExecutedContext context, IMediaType type)
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