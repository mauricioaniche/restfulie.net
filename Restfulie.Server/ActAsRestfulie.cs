using System;
using System.Web.Mvc;
using Restfulie.Server.Exceptions;
using Restfulie.Server.Marshalling;
using Restfulie.Server.Negotiation;
using Restfulie.Server.Results;
using Restfulie.Server.Unmarshalling;

namespace Restfulie.Server
{
    public class ActAsRestfulie : ActionFilterAttribute
    {
        private IResourceMarshaller marshaller;

        private readonly IMarshallerFactory marshallerFactory;
        private readonly IUnmarshallerFactory unmarshallerFactory;
        private readonly IRequestInfoFinder requestInfo;

        public string Name { get; set; }
        public Type Type { get; set; }

        public ActAsRestfulie()
        {
            marshallerFactory = new DefaultMarshallerFactory();
            unmarshallerFactory = new DefaultUnmarshallerFactory();
            requestInfo = new DefaultRequestInfoFinder();
        }

        public ActAsRestfulie(IMarshallerFactory factory,IUnmarshallerFactory unmarshallerFactory, IRequestInfoFinder finder)
        {
            this.marshallerFactory = factory;
            this.unmarshallerFactory = unmarshallerFactory;
            this.requestInfo = finder;
        }

        public override void OnResultExecuting(ResultExecutingContext filterContext)
        {
            var result = (RestfulieResult)filterContext.Result;
            result.Marshaller = marshaller;

            base.OnResultExecuting(filterContext);
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            try
            {
                marshaller = marshallerFactory.BasedOnMediaType(requestInfo.GetAcceptHeaderIn(filterContext));

                if (AResourceShouldBeUnmarshalled())
                {
                    var unmarshaller = unmarshallerFactory.BasedOnContentType(requestInfo.GetContentTypeIn(filterContext));
                    var resource = unmarshaller.ToResource(requestInfo.GetContent(filterContext), Type);
                    filterContext.ActionParameters[Name] = resource;
                }
            }
            catch(MediaTypeNotSupportedException)
            {
                filterContext.Result = new NotAcceptable();
                return;
            }
            catch(ContentTypeNotSupportedException)
            {
                filterContext.Result = new BadRequest();
                return;
            }

            base.OnActionExecuting(filterContext);
        }

        private bool AResourceShouldBeUnmarshalled()
        {
            return !string.IsNullOrEmpty(Name) && Type != null;
        }
    }
}
