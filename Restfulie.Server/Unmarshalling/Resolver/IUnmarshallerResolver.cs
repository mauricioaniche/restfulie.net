using System;
using System.Web.Mvc;

namespace Restfulie.Server.Unmarshalling.Resolver
{
    public interface IUnmarshallerResolver
    {
        void DetectIn(ActionExecutingContext context);
        bool HasResource { get; }
        Type ParameterType { get; }
        string ParameterName { get; }
    }
}