using Restfulie.Server.Marshalling.UrlGenerators;
using Restfulie.Server.Negotiation;

namespace Restfulie.Server.Marshalling
{
    public class DefaultMarshallerFactory : IMarshallerFactory
    {
        public IResourceMarshaller BasedOnMediaType(string mediaType)
        {
            if (mediaType.Contains("html"))
            {
                return new AspNetMvcMarshaller();
            }
            
            return new DefaultResourceMarshaller(
                new Relations(new AspNetMvcUrlGenerator()),
                new AcceptHeaderToSerializer().For(mediaType));
        }
    }
}