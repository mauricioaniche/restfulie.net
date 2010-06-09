using Restfulie.Server.Configuration;
using Restfulie.Server.Marshalling;
using Restfulie.Server.Unmarshalling;

namespace Restfulie.Server.MediaTypes
{
    public class HTML : IMediaType
    {
        public string[] Synonyms
        {
            get { return new[] {"text/html"}; }
        }

        public IResourceMarshaller GetMarshaller(IRestfulieConfiguration config)
        {
            return new NoMarshaller();
        }

        public IResourceUnmarshaller GetUnmarshaller(IRestfulieConfiguration config)
        {
            return new AspNetMvcUnmarshaller();
        }
    }
}
