using System.Web.Mvc;

namespace Restfulie.Server.Results.Decorators
{
    public class ContentType : ResultDecorator
    {
        private readonly string contentType;

        public ContentType(string contentType)
        {
            this.contentType = contentType;
        }

        public ContentType(string contentType, ResultDecorator nextDecorator) : base(nextDecorator)
        {
            this.contentType = contentType;
        }

        public override void Execute(ControllerContext context)
        {
            if (!string.IsNullOrEmpty(contentType))
                context.HttpContext.Response.ContentType = contentType;

            Next(context);
        }
    }
}