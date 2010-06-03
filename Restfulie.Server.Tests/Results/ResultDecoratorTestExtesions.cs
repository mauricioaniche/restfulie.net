using System;
using Restfulie.Server.Results.Decorators;

namespace Restfulie.Server.Tests.Results
{
    public static class ResultDecoratorTestExtesions
    {
        public static bool Contains(this ResultDecorator decorator, Type type)
        {
            if(decorator.GetType() == type)
            {
                return true;
            }

            return decorator.NextDecorator!=null && decorator.NextDecorator.Contains(type);
        }
    }
}
