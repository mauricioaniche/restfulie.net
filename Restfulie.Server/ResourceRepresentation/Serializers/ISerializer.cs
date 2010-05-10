using System.Collections.Generic;

namespace Restfulie.Server.ResourceRepresentation.Serializers
{
    public interface ISerializer
    {
        string Serialize(IBehaveAsResource resource, IList<Transition> transitions);
    }
}