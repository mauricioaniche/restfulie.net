using Castle.Core.Interceptor;
using Restfulie.Server.UrlGenerators;

namespace Restfulie.Server
{
    public class TransitionInterceptor : IInterceptor
    {
        private readonly Transitions transitions;
        private readonly IUrlGenerator urlGenerator;

        public TransitionInterceptor(Transitions transitions, IUrlGenerator urlGenerator)
        {
            this.transitions = transitions;
            this.urlGenerator = urlGenerator;
        }

        public void Intercept(IInvocation invocation)
        {
            var action = invocation.Method.Name;
            var controller = invocation.TargetType.Name.Replace("Controller", "");

            transitions.AddTransition(urlGenerator.For(action, controller));
        }
    }
}