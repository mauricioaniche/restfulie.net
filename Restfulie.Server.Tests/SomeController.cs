using System.Web.Mvc;

namespace Restfulie.Server.Tests
{
    public class SomeController : Controller
    {
        public ActionResult SomeSimpleAction()
        {
            return View();
        }
    }
}
