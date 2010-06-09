using System;
using System.Web.Mvc;
using Restfulie.Server.Extensions;
using System.Linq;

namespace Restfulie.Server.Unmarshalling.Resolver
{
    public class UnmarshallerResolver : IUnmarshallerResolver
    {
        public bool HasResource { get { return !string.IsNullOrEmpty(ParameterName); } }
        public Type ParameterType { get; private set; }
        public string ParameterName { get; private set; }
        
        public void DetectIn(ActionExecutingContext context)
        {
            if(IsAValidHTTPMethod(context) && ActionHasParameters(context))
            {
                var firstParameter = context.ActionDescriptor.GetParameters().First();

                ParameterType = firstParameter.ParameterType;
                ParameterName = firstParameter.ParameterName;
            }
        }

        private bool ActionHasParameters(ActionExecutingContext context)
        {
            return context.ActionDescriptor.GetParameters().Length > 0;
        }

        private bool IsArrayOfResources(ParameterDescriptor parameter)
        {
            return parameter.ParameterType.IsArray && parameter.ParameterType.GetElementType().IsAResource();
        }

        private bool IsAValidHTTPMethod(ControllerContext context)
        {
            return context.RequestContext.HttpContext.Request.HttpMethod.Equals("POST") || 
                   context.RequestContext.HttpContext.Request.HttpMethod.Equals("PUT") ||
                   context.RequestContext.HttpContext.Request.HttpMethod.Equals("PATCH");
        }

    }
}