using System;
using System.Net;
using System.Web.Mvc;
using Restfulie.Server.Results.Decorators;

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
            var decorators = new StatusCode((int)HttpStatusCode.SeeOther,
                             new Location(location));

            Execute(context, decorators);
        }
    }
}
