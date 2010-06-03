using System;
using System.Web.Mvc;
using Restfulie.Server.Results;

namespace Restfulie.Server.Tests.Fixtures
{
    public class SomeResult : RestfulieResult
    {
        public override void ExecuteResult(ControllerContext context)
        {
        }
    }
}
