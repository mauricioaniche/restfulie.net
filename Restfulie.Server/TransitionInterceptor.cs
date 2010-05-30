using System.Collections.Generic;
using Castle.Core.Interceptor;

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
            var i = 0;
            foreach (var argument in invocation.GetConcreteMethod().GetParameters())
            {
                if (invocation.Arguments[i] != null)
                {
                    values.Add(argument.Name, invocation.Arguments[i]);
                }
                i++;
            }

            relations.AddTransition(controller, action, values);
        }
    }
}