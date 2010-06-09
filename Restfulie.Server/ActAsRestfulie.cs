using System.Web.Mvc;
using Restfulie.Server.Configuration;
using Restfulie.Server.MediaTypes;
using Restfulie.Server.Negotiation;
using Restfulie.Server.Results;
using Restfulie.Server.Results.Decorators.Holders;
using Restfulie.Server.Unmarshalling;
using Restfulie.Server.Unmarshalling.Resolver;

namespace Restfulie.Server
{
    public class ActAsRestfulie : ActionFilterAttribute
    {
        private IMediaType mediaType;

        private readonly IAcceptHeaderToMediaType acceptHeader;
        private readonly IContentTypeToMediaType contentType;
        private readonly IRequestInfoFinder requestInfo;
        private readonly IResultDecoratorHolderFactory resultHolderFactory;
        private readonly IUnmarshallerResolver unmarshallerResolver;
        private readonly IRestfulieConfiguration configuration;

        public ActAsRestfulie()
        {
            var mediaTypesList = new DefaultMediaTypeList();
            acceptHeader = new AcceptHeaderToMediaType(mediaTypesList);
            contentType = new ContentTypeToMediaType(mediaTypesList);
            requestInfo = new DefaultRequestInfoFinder();
            resultHolderFactory = new ResultDecoratorHolderFactory();
            unmarshallerResolver = new UnmarshallerResolver(new AcceptPostPutAndPatchVerbs());
            configuration = new RestfulieConfiguration();
        }

        public ActAsRestfulie(IAcceptHeaderToMediaType acceptHeader, IContentTypeToMediaType contentType,
            IRequestInfoFinder finder, IResultDecoratorHolderFactory resultHolderFactory, IUnmarshallerResolver resolver,
            IRestfulieConfiguration configuration)
        {
            this.acceptHeader = acceptHeader;
            this.contentType = contentType;
            this.requestInfo = finder;
            this.resultHolderFactory = resultHolderFactory;
            this.unmarshallerResolver = resolver;
            this.configuration = configuration;
        }

        public override void OnResultExecuting(ResultExecutingContext filterContext)
        {
            var result = (RestfulieResult)filterContext.Result;
            result.MediaType = mediaType;
            result.Configuration = configuration;
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

            if (unmarshallerResolver.HasResource)
            {
                var requestMediaType = contentType.GetMediaType(requestInfo.GetContentTypeIn(filterContext));

                var resource = requestMediaType.GetUnmarshaller(configuration).Build(requestInfo.GetContent(filterContext), unmarshallerResolver.ParameterType);
                if (resource != null) filterContext.ActionParameters[unmarshallerResolver.ParameterName] = resource;
            }
        }
    }
}
