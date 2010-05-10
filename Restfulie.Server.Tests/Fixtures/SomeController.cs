using System.Web.Mvc;

namespace Restfulie.Server.Tests.Fixtures
{
    public class SomeController : Controller
    {
        public virtual ActionResult SomeSimpleAction()
        {
            return View();
        }
    }
}