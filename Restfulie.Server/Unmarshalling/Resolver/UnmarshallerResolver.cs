using System;
using System.Web.Mvc;
using Restfulie.Server.Extensions;
using System.Linq;

namespace Restfulie.Server.Unmarshalling.Resolver
{
    public class UnmarshallerResolver : IUnmarshallerResolver
    {
        private readonly IAcceptHttpVerb httpVerbs;
        public bool HasResource { get { return !string.IsNullOrEmpty(ParameterName); } }
        public Type ParameterType { get; private set; }
        public string ParameterName { get; private set; }
        
        public UnmarshallerResolver(IAcceptHttpVerb httpVerbs)
        {
            this.httpVerbs = httpVerbs;
        }

        public void DetectIn(ActionExecutingContext context)
        {
            if(httpVerbs.IsValid(context) && ActionHasParameters(context))
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

    }
}