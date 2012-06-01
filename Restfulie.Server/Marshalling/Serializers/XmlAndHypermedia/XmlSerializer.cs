using Restfulie.Server.Extensions;

namespace Restfulie.Server.Marshalling.Serializers.XmlAndHypermedia
{
    public class XmlSerializer : IResourceSerializer
    {
        #region IResourceSerializer Members

        public string Serialize(object resource)
        {
            return resource.AsXml();
        }

        #endregion
    }
}