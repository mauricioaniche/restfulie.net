using Restfulie.Server.Negotiation;

namespace Restfulie.Server.Unmarshalling
{
    public class DefaultUnmarshallerFactory : IUnmarshallerFactory
    {
        public IResourceUnmarshaller BasedOn(string contentType)
        {
            return new DefaultResourceUnmarshaller(new ContentTypeToDeserializer().For(contentType));
        }
    }
}
