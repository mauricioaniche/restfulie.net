using System;
using System.Web.Mvc;
using Restfulie.Server.Extensions;

namespace Restfulie.Server.Unmarshalling
{
    public class UnmarshallerResolver : IUnmarshallerResolver
    {
        public bool HasResource { get; private set; }
        public Type ParameterType { get; private set; }
        public string ParameterName { get; private set; }
        
        public void DetectIn(ActionExecutingContext context)
        {
            if (!IsAValidHTTPMethod(context)) return;

            foreach(var parameter in context.ActionDescriptor.GetParameters())
            {
                if (IsArrayOfResources(parameter) || parameter.ParameterType.IsAResource())
                {
                    ParameterType = parameter.ParameterType;
                    ParameterName = parameter.ParameterName;
                    HasResource = true;
                    break;
                }
            }
        }

        private static bool IsArrayOfResources(ParameterDescriptor parameter)
        {
            return parameter.ParameterType.IsArray && parameter.ParameterType.GetElementType().IsAResource();
        }

        private static bool IsAValidHTTPMethod(ControllerContext context)
        {
            return context.RequestContext.HttpContext.Request.HttpMethod.Equals("POST") || 
                context.RequestContext.HttpContext.Request.HttpMethod.Equals("PUT");
        }

    }
}
