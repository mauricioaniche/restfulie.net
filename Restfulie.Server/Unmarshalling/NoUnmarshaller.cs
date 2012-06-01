using System;

namespace Restfulie.Server.Unmarshalling
{
    public class NoUnmarshaller : IResourceUnmarshaller
    {
        #region IResourceUnmarshaller Members

        public object Build(string xml, Type objectType)
        {
            return null;
        }

        #endregion
    }
}