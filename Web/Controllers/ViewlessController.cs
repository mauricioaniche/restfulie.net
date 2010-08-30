using System.Web.Mvc;
using Restfulie.Server;
using Restfulie.Server.Results;

namespace Web.Controllers
{
	[ActAsRestfulie]
	public class ViewlessController : Controller
	{
		public virtual ActionResult Index()
		{
			return new OK();
		}
	}
}