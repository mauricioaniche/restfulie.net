using System;
using System.Collections.Generic;

namespace Restfulie.Server.Marshalling.Serializers.AtomPlusXml
{
    public class AtomPlusXmlHypermediaInserter : IHypermediaInserter
    {
        public string Insert(string content, IList<Relation> relations)
        {
            return string.Empty;
        }

        public string Insert(string content, IList<IList<Relation>> relations)
        {
            return string.Empty;
        }
    }
}
