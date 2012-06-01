using System;
using Restfulie.Server.Marshalling;
using Restfulie.Server.Unmarshalling;

namespace Restfulie.Server.MediaTypes
{
    public class UrlEncoded : MediaType
    {
        public override string[] Synonyms
        {
            get { return new[] {"application/x-www-form-urlencoded"}; }
        }

        public override IResourceMarshaller BuildMarshaller()
        {
            throw new Exception("Marshaller for UrlEncoded not available");
        }

        public override IResourceUnmarshaller BuildUnmarshaller()
        {
            return new NoUnmarshaller();
        }
    }
}