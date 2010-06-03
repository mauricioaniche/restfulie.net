using System.Net;
using System.Web.Mvc;
using Restfulie.Server.Results.Decorators;

namespace Restfulie.Server.Results
{
    public class UnsupportedMediaType : RestfulieResult
    {
        public override void ExecuteResult(ControllerContext context)
        {
            var decorators = new StatusCode((int)HttpStatusCode.UnsupportedMediaType);

            ResultHolder.Decorate(context, decorators, GetPassedResource());
        }
    }
}
