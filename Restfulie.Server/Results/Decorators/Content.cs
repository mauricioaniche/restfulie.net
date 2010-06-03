using System.Web.Mvc;

namespace Restfulie.Server.Results.Decorators
{
    public class Content : ResultDecorator
    {
        private readonly string content;

        public Content(string content)
        {
            this.content = content;
        }

        public Content(string content, ResultDecorator nextDecorator) : base(nextDecorator)
        {
            this.content = content;
        }

        public override void Execute(ControllerContext context)
        {
            if (!string.IsNullOrEmpty(content))
            {
                context.HttpContext.Response.Output.Write(content);
                context.HttpContext.Response.Output.Flush();
            }

            Next(context);
        }
    }
}