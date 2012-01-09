using System.Collections.Generic;
using Castle.DynamicProxy;

namespace Restfulie.Server
{
    class TransitionInterceptor : IInterceptor
    {
        private readonly Relations relations;

        public TransitionInterceptor(Relations relations)
        {
            this.relations = relations;
        }

        public void Intercept(IInvocation invocation)
        {
            var action = invocation.Method.Name;
            var controller = invocation.TargetType.Name.Replace("Controller", "");

            var values = new Dictionary<string, object>();
            var argValues = invocation.Arguments.GetEnumerator();
            foreach (var argument in invocation.GetConcreteMethod().GetParameters())
            {
                argValues.MoveNext();
                if (argValues.Current != null)
                {
                    values.Add(argument.Name, argValues.Current);
                }
            }

            relations.AddToAction(controller, action, values);
        }
    }
}