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

            relations.AddTransition(controller, action);
        }
    }
}