using System.Collections.Generic;
using Restfulie.Server.Request;

namespace Restfulie.Server.Marshalling.Serializers
{
    public interface IHypermediaInserter
    {
        string Insert(string content, Relations relations, IRequestInfoFinder finder);
        string Insert(string content, IList<Relations> relations, IRequestInfoFinder finder);
    }
}
