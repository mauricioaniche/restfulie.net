using System.Web.Mvc;

namespace Restfulie.Server.Results.Decorators
{
    public abstract class ResultDecorator
    {
        public ResultDecorator NextDecorator { get; private set; }

        protected ResultDecorator(ResultDecorator nextDecorator)
        {
            NextDecorator = nextDecorator;
        }

        protected ResultDecorator() {}

        public abstract void Execute(ControllerContext context);

        protected void Next(ControllerContext context)
        {
            if(NextDecorator!=null) NextDecorator.Execute(context);
        }
    }
}