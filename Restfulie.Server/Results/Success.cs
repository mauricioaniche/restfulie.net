using System.Web.Mvc;
using Restfulie.Server.Marshalling;

namespace Restfulie.Server.Results
{
    public class Success : RestfulieResult
    {
        private readonly IBehaveAsResource resource;

        public Success()
        {
        }

        public Success(IBehaveAsResource resource) : this()
        {
            this.resource = resource;
        }

        public override void ExecuteResult(ControllerContext context)
        {
            SetStatusCode(context,StatusCodes.Success);

            if(ResourceWasPassed())
            {   
                Write(context, RepresentationBuilder.Build(resource));
            }
        }

        private bool ResourceWasPassed()
        {
            return resource!=null;
        }
    }
}