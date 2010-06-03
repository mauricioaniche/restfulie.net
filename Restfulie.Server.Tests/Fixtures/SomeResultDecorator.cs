using System.Web.Mvc;
using Restfulie.Server.Results.Decorators;

namespace Restfulie.Server.Tests.Fixtures
{
    public class SomeResultDecorator : ResultDecorator
    {
        public override void Execute(ControllerContext context)
        {
            // do nothing
        }
    }
}
