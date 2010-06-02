using System;
using System.Collections.Generic;
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

        public void Build(ControllerContext context, IBehaveAsResource resource, ResponseInfo info)
        {
            viewResult.ViewData = ViewDataWithModel(null);
            viewResult.ExecuteResult(context);
        }

        public void Build(ControllerContext context, IEnumerable<IBehaveAsResource> resources, ResponseInfo info)
        {
            viewResult.ViewData = ViewDataWithModel(null);
            viewResult.ExecuteResult(context);
        }

        public void Build(ControllerContext context, string message, ResponseInfo info)
        {
            viewResult.ViewData = ViewDataWithModel(null);
            viewResult.ExecuteResult(context);
        }

        public void Build(ControllerContext context, ResponseInfo info)
        {
            viewResult.ViewData = ViewDataWithModel(null);
            viewResult.ExecuteResult(context);
        }

        private ViewDataDictionary ViewDataWithModel(object model)
        {
            return new ViewDataDictionary { Model = model };
        }
    }
}
