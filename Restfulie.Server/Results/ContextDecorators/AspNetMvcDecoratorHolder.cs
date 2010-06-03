using System.Web.Mvc;

namespace Restfulie.Server.Results.ContextDecorators
{
    public class AspNetMvcDecoratorHolder : IContextDecoratorHolder
    {
        public void Decorate(ControllerContext context, ContextDecorator decorator, object model)
        {
            var viewResult = new ViewResult
                                 {
                                     TempData = context.Controller.TempData,
                                     ViewData = context.Controller.ViewData
                                 };

            viewResult.ViewData.Model = model;
            viewResult.ExecuteResult(context);
        }
    }
}
