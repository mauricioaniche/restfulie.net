using System.Web.Mvc;

namespace Restfulie.Server.Unmarshalling.Resolver
{
    public class AcceptPostPutAndPatchVerbs : IAcceptHttpVerb
    {
        public bool IsValid(ControllerContext context)
        {
            return context.RequestContext.HttpContext.Request.HttpMethod.Equals("POST") ||
                   context.RequestContext.HttpContext.Request.HttpMethod.Equals("PUT") ||
                   context.RequestContext.HttpContext.Request.HttpMethod.Equals("PATCH");
        }
    }
}
