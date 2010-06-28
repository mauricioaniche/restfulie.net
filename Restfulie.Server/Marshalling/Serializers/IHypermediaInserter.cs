using System.Collections.Generic;

namespace Restfulie.Server.Marshalling.Serializers
{
    public interface IHypermediaInserter
    {
        string Insert(string content, IRelations relations);
        string Insert(string content, IList<IRelations> relations);
    }
}
