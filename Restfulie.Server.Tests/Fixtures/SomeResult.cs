using System.Collections.Generic;
using Restfulie.Server.Results;

namespace Restfulie.Server.Tests.Fixtures
{
    public class SomeResult : RestfulieResult
    {
        public SomeResult() { }

        public SomeResult(IBehaveAsResource resource) : base(resource)
        {
        }

        public SomeResult(IEnumerable<IBehaveAsResource> resources) : base(resources){}

        public void SetLocation(string location)
        {
            Location = location;
        }

        public override int StatusCode
        {
            get { return 123; }
        }
    }
}
