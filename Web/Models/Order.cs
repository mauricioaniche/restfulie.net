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

        public IList<Relation> GetRelations(Relations relations)
        {
            relations.Named("pay").Uses<OrdersController>().Index();

            return relations.GetAll();
        }
    }
}
