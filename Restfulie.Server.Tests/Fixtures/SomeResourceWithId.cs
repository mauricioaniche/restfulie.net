using System.Collections.Generic;

namespace Restfulie.Server.Tests.Fixtures
{
    public class SomeResourceWithId : IBehaveAsResource
    {
        public int Id { get; set; }
        public IList<Relation> GetRelations(Relations relations)
        {
            return relations.GetAll();
        }
    }
}
