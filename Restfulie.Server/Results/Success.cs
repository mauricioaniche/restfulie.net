using System;
using System.Web.Mvc;
using Restfulie.Server.Serializers;

namespace Restfulie.Server.Results
{
    public class Success : RestfulieResult
    {
        private readonly IBehaveAsResource resource;
        private readonly ISerializer serializer;

        public Success()
        {
            serializer = new DefaultXmlSerializer();
        }

        public Success(IBehaveAsResource resource) : this()
        {
            this.resource = resource;
        }

        public Success(IBehaveAsResource resource, ISerializer serializer)
        {
            this.resource = resource;
            this.serializer = serializer;
        }

        public override void ExecuteResult(ControllerContext context)
        {
            SetStatusCode(context,StatusCodes.Success);

            if(ResourceWasPassed())
            {
                Write(context, serializer.Serialize(resource));
            }
        }

        private bool ResourceWasPassed()
        {
            return resource!=null;
        }
    }
}