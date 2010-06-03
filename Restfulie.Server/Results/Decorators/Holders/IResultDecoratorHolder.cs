using System.Web.Mvc;

namespace Restfulie.Server.Results.Decorators.Holders
{
    public interface IResultDecoratorHolder
    {
        void Decorate(ControllerContext context, ResultDecorator decorator, object model);
    }
}