using System.Web.Mvc;
using Restfulie.Server.Results;

namespace Restfulie.Server.Tests.Fixtures
{
    public class SomeController : Controller
    {
        [ActAsRestfulie]
        public virtual ActionResult SomeSimpleAction()
        {
            return new OK();
        }

        [ActAsRestfulie]
        public virtual ActionResult ActionWithParameter(int id, int qty)
        {
            return new OK();
        }

        [ActAsRestfulie]
        public virtual ActionResult ActionWithResource(SomeResource resource)
        {
            return new OK(resource);
        }
    }
}