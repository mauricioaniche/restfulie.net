using System.Collections.Generic;

namespace Restfulie.Server.Marshalling.Serializers
{
    public interface ISerializer
    {
        string Serialize(IBehaveAsResource resource, IList<Transition> transitions);
    }
}