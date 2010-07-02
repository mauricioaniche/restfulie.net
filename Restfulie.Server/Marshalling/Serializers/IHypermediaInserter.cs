using System.Collections.Generic;
using Restfulie.Server.Request;

namespace Restfulie.Server.Marshalling.Serializers
{
    public interface IHypermediaInserter
    {
        string Insert(string content, Relations relations, IRequestInfoFinder requestInfo);
        string Insert(string content, IList<Relations> relations, IRequestInfoFinder requestInfo);
    }
}
