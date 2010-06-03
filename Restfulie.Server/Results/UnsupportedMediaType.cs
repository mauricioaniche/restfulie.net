using System.Net;
using System.Web.Mvc;
using Restfulie.Server.Results.ContextDecorators;

namespace Restfulie.Server.Results
{
    public class UnsupportedMediaType : RestfulieResult
    {
        public override void ExecuteResult(ControllerContext context)
        {
            var decorators = new StatusCodeDecorator((int)HttpStatusCode.UnsupportedMediaType);

            DecoratorHolder.Decorate(context, decorators, GetPassedResource());
        }
    }
}
