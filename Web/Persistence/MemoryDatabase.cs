using System;
using System.Collections.Generic;
using Web.Models;

namespace Web.Persistence
{
    public class MemoryDatabase
    {
        private static readonly IList<Item> Database = new List<Item>();

        public void Add(Item item)
        {
            Database.Add(item);
        }

        public IList<Item> List()
        {
            return Database;
        }
    }
}
