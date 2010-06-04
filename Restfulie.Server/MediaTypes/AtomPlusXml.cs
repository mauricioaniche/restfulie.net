using System;
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

        public IResourceMarshaller Marshaller
        {
            get
            {
                return new RestfulieMarshaller(
                    new Relations(new AspNetMvcUrlGenerator()), 
                    new AtomPlusXmlSerializer());
            }
        }

        public IResourceUnmarshaller Unmarshaller
        {
            get
            {
                return new RestfulieUnmarshaller(new AtomPlusXmlDeserializer());
            }
        }
    }
}
