using System.Collections.Generic;

namespace Restfulie.Server.Marshalling.Serializers
{
    public interface IResourceSerializer
    {
        string Serialize(IBehaveAsResource resource, IList<Transition> transitions);
    }
}