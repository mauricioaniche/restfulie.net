using Restfulie.Server.Extensions;

namespace Restfulie.Server.Marshalling.Serializers.XmlAndHypermedia
{
    public class XmlAndHypermediaSerializer : IResourceSerializer
    {
        public string Serialize(object resource)
        {
            return resource.AsXml();
        }
    }
}