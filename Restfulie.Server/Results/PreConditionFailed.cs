using System.Net;
using System.Web.Mvc;
using Restfulie.Server.Results.Decorators;

namespace Restfulie.Server.Results
{
    public class PreconditionFailed : RestfulieResult
    {
        public override void ExecuteResult(ControllerContext context)
        {
            var decorators = new StatusCode((int)HttpStatusCode.PreconditionFailed);

            ResultHolder.Decorate(context, decorators, GetPassedResource());
        }
    }
}
