using System.Web.Mvc;

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
                Write(context, Representation.Build(resource));
            }
        }

        private bool ResourceWasPassed()
        {
            return resource!=null;
        }
    }
}