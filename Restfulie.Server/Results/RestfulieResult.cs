using System.Web.Mvc;
using Restfulie.Server.Http;
using Restfulie.Server.MediaTypes;
using Restfulie.Server.Results.Decorators;

namespace Restfulie.Server.Results
{
    public abstract class RestfulieResult : ActionResult
    {
        protected RestfulieResult() {}

        protected RestfulieResult(object model)
        {
            Model = model;
        }

        public object Model { get; private set; }
        public IMediaType MediaType { get; set; }
        public IRequestInfoFinder RequestInfo { get; set; }

        protected string BuildContent()
        {
            return Model != null ? MediaType.BuildMarshaller().Build(Model, RequestInfo) : string.Empty;
        }

        public abstract ResultDecorator GetDecorators();

        public override void ExecuteResult(ControllerContext context)
        {
            GetDecorators().Execute(context);
        }
    }
}