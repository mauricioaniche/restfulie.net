using Restfulie.Server;

namespace Web.Models
{
    public class Item : IBehaveAsResource
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Preco { get; set; }

        public void SetRelations(Relations relations)
        {
        }
    }
}
