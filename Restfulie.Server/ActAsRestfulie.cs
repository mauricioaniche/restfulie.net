using System;
using System.Web.Mvc;
using Restfulie.Server.MediaTypes;
using Restfulie.Server.Negotiation;
using Restfulie.Server.Results;
using Restfulie.Server.Unmarshalling;

namespace Restfulie.Server
{
    public class ActAsRestfulie : ActionFilterAttribute
    {
        private IMediaType responseMediaType;
        private IMediaType requestMediaType;

        private readonly IAcceptHeaderToMediaType acceptHeader;
        private readonly IContentTypeToMediaType contentType;
        private readonly IRequestInfoFinder requestInfo;

        public string Name { get; set; }
        public Type Type { get; set; }

        public ActAsRestfulie()
        {
            var mediaTypesList = new DefaultMediaTypeList();
            acceptHeader = new AcceptHeaderToMediaType(mediaTypesList);
            contentType = new ContentTypeToMediaType(mediaTypesList);
            requestInfo = new DefaultRequestInfoFinder();
        }

        public ActAsRestfulie(IAcceptHeaderToMediaType acceptHeader, IContentTypeToMediaType contentType, IRequestInfoFinder finder)
        {
            this.acceptHeader = acceptHeader;
            this.contentType = contentType;
            this.requestInfo = finder;
        }

        public override void OnResultExecuting(ResultExecutingContext filterContext)
        {
            var result = (RestfulieResult)filterContext.Result;
            result.Marshaller = responseMediaType.Marshaller;

            base.OnResultExecuting(filterContext);
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            try
            {
                responseMediaType = acceptHeader.GetMediaType(requestInfo.GetAcceptHeaderIn(filterContext));

                if (AResourceShouldBeUnmarshalled())
                {
                    requestMediaType = contentType.GetMediaType(requestInfo.GetContentTypeIn(filterContext));
                    var resource = requestMediaType.Unmarshaller.ToResource(requestInfo.GetContent(filterContext), Type);
                    filterContext.ActionParameters[Name] = resource;
                }
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
            catch (Exception)
            {
                filterContext.Result = new InternalServerError();
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
