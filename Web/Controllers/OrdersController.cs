using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Restfulie.Server;
using Restfulie.Server.Results;
using Web.Models;

namespace Web.Controllers
{
    public class OrdersController : Controller
    {
        [ActAsRestfulie]
        public virtual ActionResult Index()
        {
            var orders = new List<IBehaveAsResource>()
                             {
                                 new Order {Amount = 333.44, Date = DateTime.Now},
                                 new Order {Amount = 555.66, Date = DateTime.Now}
                             };

            return new Success(orders);
        }

        [ActAsRestfulie(Name = "order", Type = typeof(Order))]
        public virtual ActionResult DoSomething(Order order)
        {
            return new Success(order);
        }
    }
}
