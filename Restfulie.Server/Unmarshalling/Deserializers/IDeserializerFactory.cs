namespace Restfulie.Server.Unmarshalling.Deserializers
{
    public interface IDeserializerFactory
    {
        IResourceDeserializer Create();
    }
}
