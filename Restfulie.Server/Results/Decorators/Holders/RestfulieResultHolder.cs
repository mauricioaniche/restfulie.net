using System.Web.Mvc;

namespace Restfulie.Server.Results.Decorators.Holders
{
    public class RestfulieResultHolder : IResultDecoratorHolder
    {

        public void Decorate(ControllerContext context, ResultDecorator decorator, object model)
        {
            decorator.Execute(context);
        }
    }
}