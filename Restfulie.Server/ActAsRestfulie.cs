using System;
using System.Web.Mvc;
using Restfulie.Server.MediaTypes;
using Restfulie.Server.Negotiation;
using Restfulie.Server.Results;
using Restfulie.Server.Results.Decorators.Holders;
using Restfulie.Server.Unmarshalling;

namespace Restfulie.Server
{
    public class ActAsRestfulie : ActionFilterAttribute
    {
        private IMediaType mediaType;

        private readonly IAcceptHeaderToMediaType acceptHeader;
        private readonly IContentTypeToMediaType contentType;
        private readonly IRequestInfoFinder requestInfo;
        private readonly IResultDecoratorHolderFactory resultHolderFactory;
        private IUnmarshallerResolver unmarshallerResolver;

        public ActAsRestfulie()
        {
            var mediaTypesList = new DefaultMediaTypeList();
            acceptHeader = new AcceptHeaderToMediaType(mediaTypesList);
            contentType = new ContentTypeToMediaType(mediaTypesList);
            requestInfo = new DefaultRequestInfoFinder();
            resultHolderFactory = new ResultDecoratorHolderFactory();
            unmarshallerResolver = new UnmarshallerResolver();
        }

        public ActAsRestfulie(IAcceptHeaderToMediaType acceptHeader, IContentTypeToMediaType contentType,
            IRequestInfoFinder finder, IResultDecoratorHolderFactory resultHolderFactory, IUnmarshallerResolver resolver)
        {
            this.acceptHeader = acceptHeader;
            this.contentType = contentType;
            this.requestInfo = finder;
            this.resultHolderFactory = resultHolderFactory;
            this.unmarshallerResolver = resolver;
        }

        public override void OnResultExecuting(ResultExecutingContext filterContext)
        {
            var result = (RestfulieResult)filterContext.Result;
            result.MediaType = mediaType;
            result.ResultHolder = resultHolderFactory.BasedOn(mediaType);

            base.OnResultExecuting(filterContext);
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            try
            {
                GetMediaType(filterContext);
                DoUnmarshalling(filterContext);
            }
            catch(AcceptHeaderNotSupportedException)
            {
                filterContext.Result = new NotAcceptable();
                return;
            }
            catch(ContentTypeNotSupportedException)
            {
                filterContext.Result = new UnsupportedMediaType();
                return;
            }
            catch(UnmarshallingException)
            {
                filterContext.Result = new BadRequest();
                return;
            }

            base.OnActionExecuting(filterContext);
        }

        private void GetMediaType(ControllerContext filterContext)
        {
            mediaType = acceptHeader.GetMediaType(requestInfo.GetAcceptHeaderIn(filterContext));
        }

        private void DoUnmarshalling(ActionExecutingContext filterContext)
        {
            unmarshallerResolver.DetectIn(filterContext);

            var requestMediaType = contentType.GetMediaType(requestInfo.GetContentTypeIn(filterContext));

            if (unmarshallerResolver.HasResource)
            {
                var resource = requestMediaType.Unmarshaller.ToResource(requestInfo.GetContent(filterContext), unmarshallerResolver.ParameterType);
                if (resource != null) filterContext.ActionParameters[unmarshallerResolver.ParameterName] = resource;
            }
            else if (unmarshallerResolver.HasListOfResources)
            {
                var resources = requestMediaType.Unmarshaller.ToListOfResources(requestInfo.GetContent(filterContext), unmarshallerResolver.ParameterType);
                if (resources != null) filterContext.ActionParameters[unmarshallerResolver.ParameterName] = resources;
            }
        }
    }
}
