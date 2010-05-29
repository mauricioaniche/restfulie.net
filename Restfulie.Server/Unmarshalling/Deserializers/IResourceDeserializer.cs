namespace Restfulie.Server.Unmarshalling.Deserializers
{
    public interface IResourceDeserializer
    {
        T Deserialize<T>(string xml) where T : IBehaveAsResource;
    }
}
