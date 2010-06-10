using System.Web.Mvc;
using Restfulie.Server.MediaTypes;

namespace Restfulie.Server.Results.Chooser
{
    public interface IResultChooser
    {
        ActionResult Between(ActionExecutedContext context, IMediaType type);
    }
}