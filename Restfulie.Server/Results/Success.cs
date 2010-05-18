using System.Collections.Generic;
using System.Web.Mvc;

namespace Restfulie.Server.Results
{
    public class Success : RestfulieResult
    {
        private readonly IEnumerable<IBehaveAsResource> resources;
        private readonly IBehaveAsResource resource;

        public Success()
        {
        }

        public Success(IBehaveAsResource resource) : this()
        {
            this.resource = resource;
        }

        public Success(IEnumerable<IBehaveAsResource> resources) : this()
        {
            this.resources = resources;
        }

        public override void ExecuteResult(ControllerContext context)
        {
            SetStatusCode(context,StatusCodes.Success);

            if(ResourceWasPassed())
            {   
                Write(context, Representation.Build(resource));
            }
            if(resources!=null)
            {
                Write(context, Representation.Build(resources));
            }
        }

        private bool ResourceWasPassed()
        {
            return resource!=null;
        }
    }
}