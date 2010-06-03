using System;
using System.Net;
using System.Web.Mvc;
using Restfulie.Server.Results.Decorators;

namespace Restfulie.Server.Results
{
    public class BadRequest : RestfulieResult
    {
        public override void ExecuteResult(ControllerContext context)
        {
            var decorators = new StatusCode((int) HttpStatusCode.BadRequest);

            DecoratorHolder.Decorate(context, decorators, GetPassedResource());
        }
    }
}