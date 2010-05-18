using System.Collections.Generic;

namespace Restfulie.Server
{
    public interface IBehaveAsResource
    {
        IList<Relation> GetRelations(Relations relations);
    }
}
