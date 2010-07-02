using System.Web.Mvc;
using Restfulie.Server.Configuration;
using Restfulie.Server.MediaTypes;
using Restfulie.Server.Negotiation;
using Restfulie.Server.Request;
using Restfulie.Server.Results;
using Restfulie.Server.Results.Chooser;
using Restfulie.Server.Unmarshalling;
using Restfulie.Server.Unmarshalling.Resolver;

namespace Restfulie.Server
{
    public class ActAsRestfulie : ActionFilterAttribute
    {
        private IMediaType mediaType;

        private readonly IAcceptHeaderToMediaType acceptHeader;
        private readonly IContentTypeToMediaType contentType;
        private readonly IRequestInfoFinderFactory requestInfoFactory;
        private readonly IUnmarshallerResolver unmarshallerResolver;
        private readonly IResultChooser choose;
        private IRequestInfoFinder requestInfo;

        public ActAsRestfulie()
        {
            var mediaTypesList = ConfigurationStore.Get().MediaTypes;
            acceptHeader = new AcceptHeaderToMediaType(mediaTypesList);
            contentType = new ContentTypeToMediaType(mediaTypesList);
            requestInfoFactory = new RequestInfoFinderFactory();
            unmarshallerResolver = new UnmarshallerResolver(new AcceptPostPutAndPatchVerbs());
            choose = new ResultChooser();
        }

        public ActAsRestfulie(IAcceptHeaderToMediaType acceptHeader, IContentTypeToMediaType contentType,
            IRequestInfoFinderFactory requestInfoFinderFactory, IResultChooser resultChooser, IUnmarshallerResolver resolver)
        {
            this.acceptHeader = acceptHeader;
            this.contentType = contentType;
            this.requestInfoFactory = requestInfoFinderFactory;
            this.choose = resultChooser;
            this.unmarshallerResolver = resolver;
        }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            filterContext.Result = choose.BasedOnMediaType(filterContext, mediaType);

            base.OnActionExecuted(filterContext);
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            try
            {
                GetRequestInfo(filterContext);
                GetMediaType();
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

        private void GetRequestInfo(ControllerContext filterContext)
        {
            requestInfo = requestInfoFactory.BasedOn(filterContext.HttpContext);
        }

        private void GetMediaType()
        {
            mediaType = acceptHeader.GetMediaType(requestInfo.GetAcceptHeader());
        }

        private void DoUnmarshalling(ActionExecutingContext filterContext)
        {
            unmarshallerResolver.DetectIn(filterContext);

            if (unmarshallerResolver.HasResource)
            {
                var requestMediaType = contentType.GetMediaType(requestInfo.GetContentType());

                var resource = requestMediaType.BuildUnmarshaller().Build(requestInfo.GetContent(), unmarshallerResolver.ParameterType);
                if (resource != null) filterContext.ActionParameters[unmarshallerResolver.ParameterName] = resource;
            }
        }
    }
}
