using System;
using System.Web.Mvc;

namespace Restfulie.Server.Unmarshalling.Resolver
{
    public interface IUnmarshallerResolver
    {
        bool HasResource { get; }
        Type ParameterType { get; }
        string ParameterName { get; }
        void DetectIn(ActionExecutingContext context);
    }
}