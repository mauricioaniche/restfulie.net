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
                                 new Order {Amount = 333.44, Date = DateTime.Now, Id = 111},
                                 new Order {Amount = 555.66, Date = DateTime.Now, Id = 222}
                             };

            return new Success(orders);
        }

        public virtual ActionResult ReturnOne()
        {
            return new Success(new Order {Amount = 333.44, Date = DateTime.Now, Id = 111});
        }

        public virtual ActionResult Pay(int id)
        {
            return new Success(new Order { Amount = 333.44, Date = DateTime.Now, Id = 111 });
        }

        public virtual ActionResult ReceiveAResource(Order order)
        {
            return new Created(order, "http://www.new.location.com/");
        }

        public virtual ActionResult ReceiveResources(Order[] orders)
        {
            return new Success(new List<IBehaveAsResource>(orders));
        }
    }
}
