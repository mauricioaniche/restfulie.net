using Restfulie.Server;
using Web.Controllers;

namespace Web.Models
{
    public class Item : IBehaveAsResource
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Preco { get; set; }

        public void SetRelations(Relations relations)
        {
            relations.Named("self").Uses<ItemsController>().Get(Id);
        }
    }
}
