using Restfulie.Server.Results;
using Restfulie.Server.Results.Decorators;

namespace Restfulie.Server.Tests.Fixtures
{
    public class SomeResult : RestfulieResult
    {
        public override ResultDecorator GetDecorators()
        {
            return new SomeResultDecorator();
        }
    }
}
