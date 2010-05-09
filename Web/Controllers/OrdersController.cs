using System.Web.Mvc;
using Restfulie.Server;

namespace Web.Controllers
{
    [ActAsRestfulie]
    public class OrdersController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

    }
}
