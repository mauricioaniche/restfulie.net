using System.Web.Mvc;

namespace Restfulie.Server.Results.Decorators
{
    public abstract class ResultDecorator
    {
        protected ResultDecorator(ResultDecorator nextDecorator)
        {
            NextDecorator = nextDecorator;
        }

        protected ResultDecorator() {}
        public ResultDecorator NextDecorator { get; private set; }

        public abstract void Execute(ControllerContext context);

        protected void Next(ControllerContext context)
        {
            if (NextDecorator != null)
                NextDecorator.Execute(context);
        }
    }
}