using System;
using Restfulie.Server;
using Web.Controllers;

namespace Web.Models
{
    public class Order : IBehaveAsResource
    {
        public DateTime Date { get; set; }
        public double Amount { get; set; }

        public void Transitions(Transitions transitions)
        {
            transitions.Named("pay").Uses<OrdersController>().Index();
        }
    }
}
