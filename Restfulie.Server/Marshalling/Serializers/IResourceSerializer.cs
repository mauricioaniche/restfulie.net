namespace Restfulie.Server.Marshalling.Serializers
{
    public interface IResourceSerializer
    {
        string Serialize(object resource);
    }
}