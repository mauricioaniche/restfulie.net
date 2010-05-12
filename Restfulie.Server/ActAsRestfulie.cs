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
        private IRepresentationBuilder builder;
        private readonly IRepresentationFactory factory;
        private readonly IAcceptHeaderFinder acceptHeader;

        public ActAsRestfulie()
        {
            factory = new RepresentationFactory();
            acceptHeader = new DefaultAcceptHeaderFinder();
        }

        public ActAsRestfulie(IRepresentationFactory factory, IAcceptHeaderFinder finder)
        {
            this.factory = factory;
            this.acceptHeader = finder;
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            try
            {
                builder = factory.BasedOnMediaType(acceptHeader.FindIn(filterContext));
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
            result.RepresentationBuilder = builder;

            base.OnResultExecuting(filterContext);
        }
    }
}
