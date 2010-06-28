using Restfulie.Server.Marshalling.Serializers;
using Restfulie.Server.Unmarshalling.Deserializers;

namespace Restfulie.Server.MediaTypes
{
    public class Driver : IDriver
    {
        public IResourceSerializer Serializer { get; set; }
        public IHypermediaInserter HypermediaInserter { get; set; }
        public IResourceDeserializer Deserializer { get; set; }

        public Driver(IResourceSerializer serializer, IHypermediaInserter inserter, IResourceDeserializer deserializer)
        {
            this.Serializer = serializer;
            this.HypermediaInserter = inserter;
            this.Deserializer = deserializer;
        }
    }
}
