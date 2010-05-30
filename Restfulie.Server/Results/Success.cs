using System.Collections.Generic;

namespace Restfulie.Server.Results
{
    public class Success : RestfulieResult
    {
        public Success()
        {
        }

        public Success(IBehaveAsResource resource) : base(resource)
        {
        }

        public Success(IEnumerable<IBehaveAsResource> resources) : base(resources)
        {
        }

        protected override int StatusCode
        {
            get { return (int) StatusCodes.Success; }
        }
    }
}