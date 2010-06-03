using System.Web.Mvc;

namespace Restfulie.Server.Results.ContextDecorators
{
    public class ContentDecorator : ContextDecorator
    {
        private readonly string content;

        public ContentDecorator(string content)
        {
            this.content = content;
        }

        public ContentDecorator(string content, ContextDecorator nextDecorator) : base(nextDecorator)
        {
            this.content = content;
        }

        public override void Execute(ControllerContext context)
        {
            context.HttpContext.Response.Output.Write(content);
            context.HttpContext.Response.Output.Flush();

            Next(context);
        }
    }
}
