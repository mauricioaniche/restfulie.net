using Restfulie.Server.Configuration;
using Restfulie.Server.Marshalling;
using Restfulie.Server.Marshalling.Serializers.XmlAndHypermedia;
using Restfulie.Server.Marshalling.UrlGenerators;
using Restfulie.Server.Unmarshalling;
using Restfulie.Server.Unmarshalling.Deserializers.Xml;

namespace Restfulie.Server.MediaTypes
{
    public class XmlAndHypermedia : IMediaType
    {
        public string[] Synonyms
        {
            get { return new[] {"application/xml", "text/xml" }; }
        }

        public IResourceMarshaller GetMarshaller(IRestfulieConfiguration config)
        {
            return
                new RestfulieMarshaller(new Relations(new AspNetMvcUrlGenerator()), 
                    config.GetSerializer<XmlAndHypermedia>() ?? new XmlSerializer(),
                    new XmlHypermediaInserter());
        }

        public IResourceUnmarshaller GetUnmarshaller(IRestfulieConfiguration config)
        {
            return new RestfulieUnmarshaller(config.GetDeserializer<XmlAndHypermedia>() ?? new XmlDeserializer());
        }
    }
}
