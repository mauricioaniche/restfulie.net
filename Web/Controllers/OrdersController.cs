using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Restfulie.Server;
using Restfulie.Server.Results;
using Web.Models;

namespace Web.Controllers
{
    [ActAsRestfulie]
    public class OrdersController : Controller
    {
        public virtual ActionResult Index()
        {
            var orders = new List<IBehaveAsResource>()
                             {
                                 new Order {Amount = 333.44, Date = DateTime.Now},
                                 new Order {Amount = 555.66, Date = DateTime.Now}
                             };

            return new Success(orders);
        }
    }
}
