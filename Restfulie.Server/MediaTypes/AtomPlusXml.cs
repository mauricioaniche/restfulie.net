using System;
using Restfulie.Server.Marshalling;
using Restfulie.Server.Marshalling.Serializers.AtomPlusXml;
using Restfulie.Server.Marshalling.UrlGenerators;
using Restfulie.Server.Unmarshalling;

namespace Restfulie.Server.MediaTypes
{
    public class AtomPlusXml : IMediaType
    {
        public string Name
        {
            get { return "application/atom"; }
        }

        public string[] Acronyms
        {
            get { return new[] {"application/atom", "atom"}; }
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
            get { throw new NotImplementedException(); }
        }
    }
}
