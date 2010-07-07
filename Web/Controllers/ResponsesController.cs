using System.Web.Mvc;
using Restfulie.Server;
using Restfulie.Server.Results;

namespace Web.Controllers
{
    [ActAsRestfulie]
    public class ResponsesController : Controller
    {
        public virtual ActionResult Success()
        {
            return new Success();
        }

        public virtual ActionResult BadRequest()
        {
            return new BadRequest();
        }

        public virtual ActionResult Created()
        {
            return new Created();
        }

        public virtual ActionResult InternalServerError()
        {
            return new InternalServerError();
        }

        public virtual ActionResult NotFound()
        {
            return new NotFound();
        }

        public virtual ActionResult PreconditionFailed()
        {
            return new PreconditionFailed();
        }

        public virtual ActionResult SeeOther()
        {
            return new SeeOther(Url.Action("Success"));
        }
    }
}
