using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Restfulie.Server.Marshalling.Serializers.XmlAndHypermedia
{
    public class XmlAndHypermediaSerializerFactory : ISerializerFactory
    {
        public IResourceSerializer Create()
        {
            return new XmlAndHypermediaSerializer(new DefaultInflections());
        }
    }
}
