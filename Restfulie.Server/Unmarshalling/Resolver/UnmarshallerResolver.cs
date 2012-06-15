using System;
using System.Linq;
using System.Web.Mvc;

namespace Restfulie.Server.Unmarshalling.Resolver
{
    public class UnmarshallerResolver : IUnmarshallerResolver
    {
        private readonly IAcceptHttpVerb httpVerbs;

        public UnmarshallerResolver(IAcceptHttpVerb httpVerbs)
        {
            this.httpVerbs = httpVerbs;
        }

        #region IUnmarshallerResolver Members

        public bool HasResource
        {
            get { return !string.IsNullOrEmpty(ParameterName); }
        }

        public Type ParameterType { get; private set; }
        public string ParameterName { get; private set; }

        public void DetectIn(ActionExecutingContext context)
        {
            if (httpVerbs.IsValid(context) && ActionHasParameters(context))
            {
                var firstParameter = context.ActionDescriptor.GetParameters().First();

                ParameterType = firstParameter.ParameterType;
                ParameterName = firstParameter.ParameterName;
            }
        }

        #endregion

        private bool ActionHasParameters(ActionExecutingContext context)
        {
            return context.ActionDescriptor.GetParameters().Length > 0;
        }
    }
}