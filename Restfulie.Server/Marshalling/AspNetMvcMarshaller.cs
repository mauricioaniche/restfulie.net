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
            SetViewAndTempData(context, resource);
            viewResult.ExecuteResult(context);
        }

        public void Build(ControllerContext context, IEnumerable<IBehaveAsResource> resources, ResponseInfo info)
        {
            SetViewAndTempData(context, resources);
            viewResult.ExecuteResult(context);
        }

        public void Build(ControllerContext context, ResponseInfo info)
        {
            SetViewAndTempData(context, null);
            viewResult.ExecuteResult(context);
        }

        private void SetViewAndTempData(ControllerContext context, object model)
        {
            viewResult.ViewData = context.Controller.ViewData;
            viewResult.ViewData.Model = model;
            viewResult.TempData = context.Controller.TempData;
        }
    }
}
