using System.Web.Mvc;
using Restfulie.Server.MediaTypes;
using Restfulie.Server.Results.Decorators;

namespace Restfulie.Server.Results
{
    public abstract class RestfulieResult : ActionResult
    {
        public object Model { get; private set;}

        public IMediaType MediaType { get; set; }

        protected RestfulieResult()
        {
        }

        protected RestfulieResult(object model)
        {
            this.Model = model;
        }

        protected string BuildContent()
        {
            return Model != null ? MediaType.Marshaller.Build(Model) : string.Empty;
        }

        public abstract ResultDecorator GetDecorators();

        public override void ExecuteResult(ControllerContext context)
        {
            GetDecorators().Execute(context);
        }
    }
}
