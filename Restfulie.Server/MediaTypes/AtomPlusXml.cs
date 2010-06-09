using Castle.Core.Configuration;
using Restfulie.Server.Configuration;
using Restfulie.Server.Marshalling;
using Restfulie.Server.Marshalling.Serializers.AtomPlusXml;
using Restfulie.Server.Marshalling.UrlGenerators;
using Restfulie.Server.Unmarshalling;
using Restfulie.Server.Unmarshalling.Deserializers.AtomPlusXml;

namespace Restfulie.Server.MediaTypes
{
    public class AtomPlusXml : IMediaType
    {

        public string[] Synonyms
        {
            get { return new[] {"application/atom+xml", "atom+xml"}; }
        }

        public IResourceMarshaller GetMarshaller(IRestfulieConfiguration config)
        {
            return new RestfulieMarshaller(
                new Relations(new AspNetMvcUrlGenerator()), 
                config.GetSerializer<AtomPlusXml>() ?? new AtomPlusXmlSerializer(),
                new AtomPlusXmlHypermediaInserter());
        }

        public IResourceUnmarshaller GetUnmarshaller(IRestfulieConfiguration config)
        {
            return new RestfulieUnmarshaller(config.GetDeserializer<AtomPlusXml>() ?? new AtomPlusXmlDeserializer());
        }
    }
}
