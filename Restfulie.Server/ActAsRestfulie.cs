using System;
using System.Web.Mvc;
using Restfulie.Server.Exceptions;
using Restfulie.Server.MediaTypes;
using Restfulie.Server.Negotiation;
using Restfulie.Server.Results;

namespace Restfulie.Server
{
    public class ActAsRestfulie : ActionFilterAttribute
    {
        private IMediaType responseMediaType;
        private IMediaType requestMediaType;

        private readonly IContentNegotiation contentNegotiation;
        private readonly IRequestInfoFinder requestInfo;

        public string Name { get; set; }
        public Type Type { get; set; }

        public ActAsRestfulie()
        {
            contentNegotiation = new DefaultContentNegotiation();
            requestInfo = new DefaultRequestInfoFinder();
        }

        public ActAsRestfulie(IContentNegotiation contentNegotiation, IRequestInfoFinder finder)
        {
            this.contentNegotiation = contentNegotiation;
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
                responseMediaType = contentNegotiation.ForRequest(requestInfo.GetAcceptHeaderIn(filterContext));

                if (AResourceShouldBeUnmarshalled())
                {
                    requestMediaType = contentNegotiation.ForResponse(requestInfo.GetContentTypeIn(filterContext));
                    var resource = requestMediaType.Unmarshaller.ToResource(requestInfo.GetContent(filterContext), Type);
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

        private bool AResourceShouldBeUnmarshalled()
        {
            return !string.IsNullOrEmpty(Name) && Type != null;
        }
    }
}
