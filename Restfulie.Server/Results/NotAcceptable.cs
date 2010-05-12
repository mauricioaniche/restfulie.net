using System.Web.Mvc;

namespace Restfulie.Server.Results
{
    public class NotAcceptable : RestfulieResult
    {
        public override void ExecuteResult(ControllerContext context)
        {
            SetStatusCode(context, StatusCodes.NotAcceptable);
        }
    }
}
