using System.Collections.Generic;
using System.Web.Mvc;

namespace Restfulie.Server.Marshalling
{
    public interface IResourceMarshaller
    {
        void Build(ControllerContext context, IBehaveAsResource resource, ResponseInfo info);
        void Build(ControllerContext context, IEnumerable<IBehaveAsResource> resources, ResponseInfo info);
        void Build(ControllerContext context, string message, ResponseInfo info);
        void Build(ControllerContext context, ResponseInfo info);
    }
}