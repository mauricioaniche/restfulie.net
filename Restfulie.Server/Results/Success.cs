using System.Collections.Generic;
using System.Net;
using System.Web.Mvc;
using Restfulie.Server.Results.ContextDecorators;
using System.Linq;

namespace Restfulie.Server.Results
{
    public class Success : RestfulieResult
    {
        public Success() { }
        public Success(IBehaveAsResource resource) : base(resource) { }
        public Success(IEnumerable<IBehaveAsResource> resources) : base(resources) { }

        public override void ExecuteResult(ControllerContext context)
        {
            var decorators = new StatusCodeDecorator((int)HttpStatusCode.OK,
                             new ContentTypeDecorator(MediaType.Synonyms.First(),
                             new ContentDecorator(BuildContent())));

            DecoratorHolder.Decorate(context, decorators, GetPassedResource());
        }
    }
}