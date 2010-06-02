using System.Web.Mvc;

namespace Restfulie.Server.Marshalling
{
    public class AspNetMvcMarshaller : IResourceMarshaller
    {
        public void Build(ControllerContext context, MarshallingInfo info)
        {
            new ViewResult
                       {
                           ViewName = null,
                           MasterName = null,
                           TempData = null,
                           ViewData = new ViewDataDictionary {Model = info.Resource}
                       }.ExecuteResult(context);
        }
    }
}
