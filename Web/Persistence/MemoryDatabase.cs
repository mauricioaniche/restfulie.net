using System.Collections.Generic;
using Web.Models;

namespace Web.Persistence
{
    public class MemoryDatabase
    {
        private static readonly IList<Item> Items = new List<Item>();

        public void Add(Item item)
        {
            item.Id = Items.Count + 1;
            Items.Add(item);
        }

        public IList<Item> List()
        {
            return Items;
        }
    }
}
