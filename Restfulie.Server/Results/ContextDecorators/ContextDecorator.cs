using System.Web.Mvc;

namespace Restfulie.Server.Results.ContextDecorators
{
    public abstract class ContextDecorator
    {
        private readonly ContextDecorator nextDecorator;

        protected ContextDecorator(ContextDecorator nextDecorator)
        {
            this.nextDecorator = nextDecorator;
        }

        protected ContextDecorator() {}

        public abstract void Execute(ControllerContext context);

        protected void Next(ControllerContext context)
        {
            if(nextDecorator!=null) nextDecorator.Execute(context);
        }
    }
}
