using System.Collections.Generic;

namespace Restfulie.Server.Marshalling.Serializers
{
    public interface IResourceSerializer
    {
        string Serialize(IBehaveAsResource resource, IList<Relation> transitions);
        string Serialize(IDictionary<IBehaveAsResource, IList<Relation>> resources, string rootName);
    }
}