using Restfulie.Server.Marshalling;
using Restfulie.Server.Marshalling.Serializers.XmlAndHypermedia;
using Restfulie.Server.Marshalling.UrlGenerators;
using Restfulie.Server.Unmarshalling;
using Restfulie.Server.Unmarshalling.Deserializers.Xml;

namespace Restfulie.Server.MediaTypes
{
    public class XmlAndHypermedia : IMediaType
    {
        public string Name
        {
            get { return "application/xml"; }
        }

        public string[] Acronyms
        {
            get { return new[] {"application/xml", "text/xml", "xml"}; }
        }

        public IResourceMarshaller Marshaller
        {
            get
            {
                return
                    new RestfulieMarshaller(new Relations(new AspNetMvcUrlGenerator()), 
                        new XmlAndHypermediaSerializer(new DefaultInflections()));
            }
        }

        public IResourceUnmarshaller Unmarshaller
        {
            get 
            {
                return new DefaultResourceUnmarshaller(new XmlDeserializer());
            }
        }
    }
}
