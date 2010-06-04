using System;
using System.Web.Mvc;

namespace Restfulie.Server.Unmarshalling
{
    public class UnmarshallerResolver
    {
        public bool HasResource { get; private set; }
        public Type Type { get; private set; }
        public string ParameterName { get; private set; }

        public void DetectIn(ActionExecutingContext context)
        {
            if (!IsAValidHTTPMethod(context)) return;

            foreach(var parameter in context.ActionDescriptor.GetParameters())
            {
                if(parameter.ParameterType == typeof(IBehaveAsResource) || parameter.ParameterType.GetInterface(typeof(IBehaveAsResource).FullName) != null)
                {
                    Type = parameter.ParameterType;
                    ParameterName = parameter.ParameterName;
                    HasResource = true;
                    break;
                }
            }
        }

        private static bool IsAValidHTTPMethod(ControllerContext context)
        {
            return context.RequestContext.HttpContext.Request.HttpMethod.Equals("POST") || 
                context.RequestContext.HttpContext.Request.HttpMethod.Equals("PUT");
        }

    }
}
