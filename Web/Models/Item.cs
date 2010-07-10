using Restfulie.Server;
using Web.Controllers;

namespace Web.Models
{
    public class Item : IBehaveAsResource
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Price { get; set; }

        public void SetRelations(Relations relations)
        {
            relations.Named("self").Uses<ItemsController>().Get(Id);
            relations.Named("origin").At("http://www.some-fabric.com/");
        }
    }
}
