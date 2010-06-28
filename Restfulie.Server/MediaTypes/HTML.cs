using System;
using Restfulie.Server.Marshalling;
using Restfulie.Server.Marshalling.Serializers;
using Restfulie.Server.Unmarshalling;
using Restfulie.Server.Unmarshalling.Deserializers;

namespace Restfulie.Server.MediaTypes
{
    public class HTML : IMediaType
    {
        public IDriver Driver { get; set; }

        public string[] Synonyms
        {
            get { return new[] {"text/html"}; }
        }

        public IResourceMarshaller BuildMarshaller()
        {
            throw new Exception("HTML should be marshalled by ASP.NET MVC! :-(");
        }

        public IResourceUnmarshaller BuildUnmarshaller()
        {
            return new NoUnmarshaller();
        }
    }
}
