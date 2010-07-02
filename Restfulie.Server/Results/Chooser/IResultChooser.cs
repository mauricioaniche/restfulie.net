using System.Web.Mvc;
using Restfulie.Server.MediaTypes;
using Restfulie.Server.Request;

namespace Restfulie.Server.Results.Chooser
{
    public interface IResultChooser
    {
        ActionResult BasedOnMediaType(ActionExecutedContext context, IMediaType type,
                                      IRequestInfoFinder requestInfoFinder);
    }
}