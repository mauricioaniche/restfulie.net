using System;
using Restfulie.Server.Marshalling;
using Restfulie.Server.Unmarshalling;

namespace Restfulie.Server.MediaTypes
{
    public class HTML : MediaType
    {
        public override string[] Synonyms
        {
            get { return new[] {"text/html"}; }
        }

        public override IResourceMarshaller BuildMarshaller()
        {
            throw new Exception("HTML should be marshalled by ASP.NET MVC! :-(");
        }

        public override IResourceUnmarshaller BuildUnmarshaller()
        {
            return new NoUnmarshaller();
        }
    }
}