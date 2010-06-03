using System.Web.Mvc;

namespace Restfulie.Server.Results.ContextDecorators
{
    public interface IContextDecoratorHolder
    {
        void Decorate(ControllerContext context, ContextDecorator decorator, object model);
    }
}
