using System.Web.Mvc;

namespace Restfulie.Server.Results.Decorators
{
    public abstract class ResultDecorator
    {
        private readonly ResultDecorator nextDecorator;

        protected ResultDecorator(ResultDecorator nextDecorator)
        {
            this.nextDecorator = nextDecorator;
        }

        protected ResultDecorator() {}

        public abstract void Execute(ControllerContext context);

        protected void Next(ControllerContext context)
        {
            if(nextDecorator!=null) nextDecorator.Execute(context);
        }
    }
}