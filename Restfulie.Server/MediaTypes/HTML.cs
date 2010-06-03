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

        public IResourceMarshaller Marshaller
        {
            get
            {
                return new NoMarshaller();
            }
        }

        public IResourceUnmarshaller Unmarshaller
        {
            get
            {
                return new AspNetMvcUnmarshaller();
            }
        }
    }
}
