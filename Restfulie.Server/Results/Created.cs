using System;

namespace Restfulie.Server.Results
{
    public class Created : RestfulieResult
    {
        public Created() {}
        public Created(IBehaveAsResource resource, string location) : base(resource)
        {
            base.Location = location;
        }

        protected override int StatusCode
        {
            get { return (int)StatusCodes.Created; }
        }
    }
}
