using System.Collections.Generic;

namespace Restfulie.Server
{
    public interface IRelations
    {
        IList<Relation> GetAll();
    }
}
