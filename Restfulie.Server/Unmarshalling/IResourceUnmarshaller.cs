using System;

namespace Restfulie.Server.Unmarshalling
{
    public interface IResourceUnmarshaller
    {
        object Build(string xml, Type objectType);
    }
}