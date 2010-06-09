using System.Web.Mvc;
using Restfulie.Server.Configuration;
using Restfulie.Server.MediaTypes;
using Restfulie.Server.Results.Decorators;
using Restfulie.Server.Results.Decorators.Holders;

namespace Restfulie.Server.Results
{
    public abstract class RestfulieResult : ActionResult
    {
        private readonly object model;

        public IMediaType MediaType { get; set; }
        public IResultDecoratorHolder ResultHolder { get; set; }
        public IRestfulieConfiguration Configuration { get; set; }

        protected RestfulieResult()
        {
        }

        protected RestfulieResult(object model)
        {
            this.model = model;
        }

        protected void Execute(ControllerContext context, ResultDecorator decorator)
        {
            ResultHolder.Decorate(context, decorator, model);
        }

        protected string BuildContent()
        {
            return model != null ? MediaType.GetMarshaller(Configuration).Build(model) : string.Empty;
        }

        public abstract ResultDecorator GetDecorators();

        public override void ExecuteResult(ControllerContext context)
        {
            Execute(context, GetDecorators());
        }
    }
}
