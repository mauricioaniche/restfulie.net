using System;
using Restfulie.Server.Marshalling;
using Restfulie.Server.Unmarshalling;

namespace Restfulie.Server.MediaTypes
{
    public class UrlEncoded : IMediaType
    {
        public string[] Synonyms
        {
            get { return new[] {"application/x-www-form-urlencoded"}; }
        }

        public IResourceMarshaller BuildMarshaller()
        {
            throw new Exception("Marshaller for UrlEncoded not available");
        }

        public IResourceUnmarshaller BuildUnmarshaller()
        {
            return new NoUnmarshaller();
        }

        public IDriver Driver
        {
            get; set;
        }
    }
}
