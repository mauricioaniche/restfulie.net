using System.Web.Mvc;
using Restfulie.Server.Results;

namespace Restfulie.Server.Extensions
{
    public static class ActionResultExtensions
    {
        public static bool IsRestfulieResult(this ActionResult actionResult)
        {
            return actionResult is RestfulieResult;
        }
    }
}