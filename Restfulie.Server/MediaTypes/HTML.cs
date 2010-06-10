using System;
using Restfulie.Server.Marshalling;
using Restfulie.Server.Marshalling.Serializers;
using Restfulie.Server.Unmarshalling;
using Restfulie.Server.Unmarshalling.Deserializers;

namespace Restfulie.Server.MediaTypes
{
    public class HTML : IMediaType
    {
        public IResourceSerializer Serializer { get; set; }
        public IHypermediaInserter Hypermedia { get; set; }
        public IResourceDeserializer Deserializer { get; set; }

        public string[] Synonyms
        {
            get { return new[] {"text/html"}; }
        }

        public IResourceMarshaller Marshaller
        {
            get { throw new Exception("HTML should be marshalled by ASP.NET MVC! :-("); }
        }

        public IResourceUnmarshaller Unmarshaller
        {
            get { return new AspNetMvcUnmarshaller(); }
        }
    }
}
