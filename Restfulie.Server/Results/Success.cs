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
            SetContentType(context, Marshaller.MediaType);
            WriteResource(context);
        }

        private void WriteResource(ControllerContext context)
        {
            if(ResourceWasPassed())
            {   
                Write(context, Marshaller.Build(resource));
            }
            if(ResourcesWerePassed())
            {
                Write(context, Marshaller.Build(resources));
            }
        }

        private bool ResourcesWerePassed()
        {
            return resources!=null;
        }

        private bool ResourceWasPassed()
        {
            return resource!=null;
        }
    }
}