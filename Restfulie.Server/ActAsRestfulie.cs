using System;
using System.Web.Mvc;
using Restfulie.Server.Exceptions;
using Restfulie.Server.Marshalling;
using Restfulie.Server.Negotitation;
using Restfulie.Server.Results;

namespace Restfulie.Server
{
    public class ActAsRestfulie : ActionFilterAttribute
    {
        private IResourceRepresentation builder;
        private readonly IRepresentationFactory marshaller;
        private readonly IAcceptHeaderFinder acceptHeader;

        public ActAsRestfulie()
        {
            marshaller = new DefaultRepresentationFactory();
            acceptHeader = new DefaultAcceptHeaderFinder();
        }

        public ActAsRestfulie(IRepresentationFactory factory, IAcceptHeaderFinder finder)
        {
            this.marshaller = factory;
            this.acceptHeader = finder;
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            try
            {
                builder = marshaller.BasedOnMediaType(acceptHeader.FindIn(filterContext));
            }
            catch(MediaTypeNotSupportedException)
            {
                filterContext.Result = new NotAcceptable();
                return;
            }

            base.OnActionExecuting(filterContext);
        }

        public override void OnResultExecuting(ResultExecutingContext filterContext)
        {
            var result = (RestfulieResult)filterContext.Result;
            result.MarshallerBuilder = builder;

            base.OnResultExecuting(filterContext);
        }
    }
}
