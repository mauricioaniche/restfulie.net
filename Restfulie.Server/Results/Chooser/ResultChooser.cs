using System.Web.Mvc;
using Restfulie.Server.Extensions;
using Restfulie.Server.Http;
using Restfulie.Server.MediaTypes;

namespace Restfulie.Server.Results.Chooser
{
    public class ResultChooser : IResultChooser
    {
        #region IResultChooser Members

        public ActionResult BasedOnMediaType(ActionExecutedContext context, IMediaType type, IRequestInfoFinder requestInfoFinder)
        {
            if (!context.Result.IsRestfulieResult())
                return context.Result;

            if (type is HTML && (context.Result is OK || context.Result is Created))
                return AspNetResult(context);

            return RestfulieResult(context, type, requestInfoFinder);
        }

        #endregion

        private ActionResult RestfulieResult(ActionExecutedContext context, IMediaType type, IRequestInfoFinder requestInfoFinder)
        {
            var result = (RestfulieResult) context.Result;
            result.MediaType = type;
            result.RequestInfo = requestInfoFinder;

            return result;
        }

        private ActionResult AspNetResult(ActionExecutedContext context)
        {
            var result = (RestfulieResult) context.Result;

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