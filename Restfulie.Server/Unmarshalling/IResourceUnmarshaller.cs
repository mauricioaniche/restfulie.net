using System;

namespace Restfulie.Server.Unmarshalling
{
    public interface IResourceUnmarshaller
    {
        IBehaveAsResource ToResource(string xml, Type objectType);
    }
}
