namespace Restfulie.Server.Marshalling.Serializers
{
    public interface ISerializerFactory
    {
        IResourceSerializer Create();
    }
}