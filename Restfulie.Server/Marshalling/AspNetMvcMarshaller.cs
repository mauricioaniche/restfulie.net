using System;
using System.Web.Mvc;

namespace Restfulie.Server.Marshalling
{
    public class AspNetMvcMarshaller : IResourceMarshaller
    {
        private readonly ViewResult viewResult;

        public AspNetMvcMarshaller(ViewResult viewResult)
        {
            this.viewResult = viewResult;
        }

        public void Build(ControllerContext context, MarshallingInfo info)
        {
            viewResult.ViewData = BasedOn(info);
            viewResult.ExecuteResult(context);
        }

        private ViewDataDictionary BasedOn(MarshallingInfo info)
        {
            if (info.HasResource())
            {
                return new ViewDataDictionary { Model = info.Resource };
            }
            if (info.HasResources())
            {
                return new ViewDataDictionary { Model = info.Resources };
            }
            if (info.HasMessage())
            {
                return new ViewDataDictionary { Model = info.Message };
            }

            return new ViewDataDictionary();
        }
    }
}
