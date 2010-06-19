using System.Web.Mvc;
using Restfulie.Server.MediaTypes;

namespace Restfulie.Server.Results.Chooser
{
    public interface IResultChooser
    {
        ActionResult BasedOnMediaType(ActionExecutedContext context, IMediaType type);
    }
}