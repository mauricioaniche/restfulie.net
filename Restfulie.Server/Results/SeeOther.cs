using System;
using System.Net;
using System.Web.Mvc;
using Restfulie.Server.Results.ContextDecorators;

namespace Restfulie.Server.Results
{
    public class SeeOther : RestfulieResult
    {
        private readonly string location;

        public SeeOther(string location)
        {
            this.location = location;
        }

        public override void ExecuteResult(ControllerContext context)
        {
            var decorators = new StatusCodeDecorator((int)HttpStatusCode.SeeOther,
                             new LocationDecorator(location));

            DecoratorHolder.Decorate(context, decorators, GetPassedResource());
        }
    }
}
