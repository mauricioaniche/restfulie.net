using System.Web.Mvc;

namespace Restfulie.Server.Results.Decorators.Holders
{
    public class AspNetMvcResultHolder : IResultDecoratorHolder
    {
        public void Decorate(ControllerContext context, ResultDecorator decorator, object model)
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