using Castle.Core.Interceptor;

namespace Restfulie.Server
{
    class TransitionInterceptor : IInterceptor
    {
        private readonly Transitions transitions;

        public TransitionInterceptor(Transitions transitions)
        {
            this.transitions = transitions;
        }

        public void Intercept(IInvocation invocation)
        {
            transitions.AddTransition();
        }
    }
}