using System.Web.Mvc;

namespace Restfulie.Server.Tests
{
    public class SomeController : Controller
    {
        public virtual ActionResult SomeSimpleAction()
        {
            return View();
        }
    }
}
