using Restfulie.Server.Negotitation;

namespace Restfulie.Server.Unmarshalling
{
    public class ResourceUnmarshallerFactory : IUnmarshallerFactory
    {
        public IResourceUnmarshaller BasedOn(string contentType)
        {
            return new DefaultResourceUnmarshaller(new ContentTypeToDeserializer().For(contentType));
        }
    }
}
