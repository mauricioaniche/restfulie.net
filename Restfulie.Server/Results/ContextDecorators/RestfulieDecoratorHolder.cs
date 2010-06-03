using System.Web.Mvc;

namespace Restfulie.Server.Results.ContextDecorators
{
    public class RestfulieDecoratorHolder : IContextDecoratorHolder
    {

        public void Decorate(ControllerContext context, ContextDecorator decorator, object model)
        {
            decorator.Execute(context);
        }
    }
}
