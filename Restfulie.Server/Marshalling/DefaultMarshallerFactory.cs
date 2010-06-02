using System.Web.Mvc;
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
                return new AspNetMvcMarshaller(new ViewResult());
            }
            
            return new RestfulieMarshaller(
                new Relations(new AspNetMvcUrlGenerator()),
                new AcceptHeaderToSerializer().For(mediaType));
        }
    }
}