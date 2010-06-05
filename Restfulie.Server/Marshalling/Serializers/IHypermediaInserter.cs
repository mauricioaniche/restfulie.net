using System.Collections.Generic;

namespace Restfulie.Server.Marshalling.Serializers
{
    public interface IHypermediaInserter
    {
        string Insert(string content, IList<Relation> relations);
        string Insert(string content, IList<IList<Relation>> relations);
    }
}
