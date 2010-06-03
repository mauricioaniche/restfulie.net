using System.Net;
using System.Web.Mvc;
using Restfulie.Server.Results.ContextDecorators;

namespace Restfulie.Server.Results
{
    public class PreconditionFailed : RestfulieResult
    {
        public override void ExecuteResult(ControllerContext context)
        {
            var decorators = new StatusCodeDecorator((int)HttpStatusCode.PreconditionFailed);

            DecoratorHolder.Decorate(context, decorators, GetPassedResource());
        }
    }
}
