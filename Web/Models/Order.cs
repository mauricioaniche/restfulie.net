using System;
using System.Collections.Generic;
using Restfulie.Server;
using Web.Controllers;

namespace Web.Models
{
    public class Order : IBehaveAsResource
    {
        public DateTime Date { get; set; }
        public double Amount { get; set; }
        public int Id { get; set; }

        public IList<Relation> GetRelations(Relations relations)
        {
            relations.Named("all").Uses<OrdersController>().Index();
            relations.Named("pay").Uses<OrdersController>().Pay(Id);
            relations.Named("something").Uses<OrdersController>().ReceiveAResource(null);

            return relations.GetAll();
        }
    }
}
