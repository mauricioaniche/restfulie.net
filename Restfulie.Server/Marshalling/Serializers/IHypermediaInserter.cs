using System.Collections.Generic;

namespace Restfulie.Server.Marshalling.Serializers
{
    public interface IHypermediaInserter
    {
        string Insert(string content, Relations relations);
        string Insert(string content, IList<Relations> relations);
    }
}
