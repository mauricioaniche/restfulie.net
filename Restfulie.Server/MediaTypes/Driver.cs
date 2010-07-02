using Restfulie.Server.Marshalling.Serializers;
using Restfulie.Server.Unmarshalling.Deserializers;

namespace Restfulie.Server.MediaTypes
{
    public class Driver : IDriver
    {
        public IResourceSerializer Serializer { get; set; }
        public IHypermediaInjector HypermediaInjector { get; set; }
        public IResourceDeserializer Deserializer { get; set; }

        public Driver(IResourceSerializer serializer, IHypermediaInjector injector, IResourceDeserializer deserializer)
        {
            this.Serializer = serializer;
            this.HypermediaInjector = injector;
            this.Deserializer = deserializer;
        }
    }
}
