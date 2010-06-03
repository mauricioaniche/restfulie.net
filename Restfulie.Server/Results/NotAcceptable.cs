using System.Net;
using System.Web.Mvc;
using Restfulie.Server.Results.ContextDecorators;

namespace Restfulie.Server.Results
{
    public class NotAcceptable : RestfulieResult
    {
        public override void ExecuteResult(ControllerContext context)
        {
            var decorators = new StatusCodeDecorator((int)HttpStatusCode.NotAcceptable);

            DecoratorHolder.Decorate(context, decorators, GetPassedResource());
        }
    }
}
