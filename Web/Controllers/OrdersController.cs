using System;
using System.Web.Mvc;
using Restfulie.Server;
using Restfulie.Server.Results;
using Web.Models;

namespace Web.Controllers
{
    [ActAsRestfulie]
    public class OrdersController : Controller
    {
        public ActionResult Index()
        {
            var order = new Order {Amount = 333.44, Date = DateTime.Now};
            return new Success(order);
        }

    }
}
