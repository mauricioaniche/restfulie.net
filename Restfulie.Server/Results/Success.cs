using System.Web.Mvc;
using Restfulie.Server.Marshalling;
using Restfulie.Server.Marshalling.Serializers;

namespace Restfulie.Server.Results
{
    public class Success : RestfulieResult
    {
        private readonly IBehaveAsResource resource;
        private readonly IRepresentationBuilder builder;

        public Success()
        {
            builder = new RepresentationFactory().GetDefault();
        }

        public Success(IBehaveAsResource resource) : this()
        {
            this.resource = resource;
        }

        public Success(IBehaveAsResource resource, ISerializer serializer)
        {
            this.resource = resource;
            this.builder = new RepresentationFactory().GetDefault(serializer);
        }

        public Success(IBehaveAsResource resource, IRepresentationBuilder builder)
        {
            this.resource = resource;
            this.builder = builder;
        }

        public override void ExecuteResult(ControllerContext context)
        {
            SetStatusCode(context,StatusCodes.Success);

            if(ResourceWasPassed())
            {   
                Write(context, builder.Build(resource));
            }
        }

        private bool ResourceWasPassed()
        {
            return resource!=null;
        }
    }
}