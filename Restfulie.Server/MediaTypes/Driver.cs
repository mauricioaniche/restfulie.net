using Restfulie.Server.Marshalling.Serializers;
using Restfulie.Server.Unmarshalling.Deserializers;

namespace Restfulie.Server.MediaTypes
{
    public class Driver : IDriver
    {
        public Driver(IResourceSerializer serializer, IHypermediaInjector injector, IResourceDeserializer deserializer)
        {
            Serializer = serializer;
            HypermediaInjector = injector;
            Deserializer = deserializer;
        }

        #region IDriver Members

        public IResourceSerializer Serializer { get; set; }
        public IHypermediaInjector HypermediaInjector { get; set; }
        public IResourceDeserializer Deserializer { get; set; }

        #endregion
    }
}