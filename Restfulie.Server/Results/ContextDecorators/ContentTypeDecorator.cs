using System.Web.Mvc;

namespace Restfulie.Server.Results.ContextDecorators
{
    public class ContentTypeDecorator : ContextDecorator
    {
        private readonly string contentType;

        public ContentTypeDecorator(string contentType)
        {
            this.contentType = contentType;
        }

        public ContentTypeDecorator(string contentType, ContextDecorator nextDecorator) : base(nextDecorator)
        {
            this.contentType = contentType;
        }

        public override void Execute(ControllerContext context)
        {
            context.HttpContext.Response.ContentType = contentType;
            Next(context);
        }
    }
}
