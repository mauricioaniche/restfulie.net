using System.Web.Mvc;

namespace Restfulie.Server.Marshalling
{
    public interface IResourceMarshaller
    {
        void Build(ControllerContext context, MarshallingInfo info);
    }
}