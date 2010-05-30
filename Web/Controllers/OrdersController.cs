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
                                 new Order {Amount = 333.44, Date = DateTime.Now, Id = 111},
                                 new Order {Amount = 555.66, Date = DateTime.Now, Id = 222}
                             };

            return new Success(orders);
        }

        [ActAsRestfulie]
        public virtual ActionResult ReturnOne()
        {
            return new Success(new Order {Amount = 333.44, Date = DateTime.Now, Id = 111});
        }

        [ActAsRestfulie]
        public virtual ActionResult Pay(int id)
        {
            return new Success(new Order { Amount = 333.44, Date = DateTime.Now, Id = 111 });
        }

        [ActAsRestfulie(Name = "order", Type = typeof(Order))]
        public virtual ActionResult ReceiveAResource(Order order)
        {
            return new Success(order);
        }
    }
}
