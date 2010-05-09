namespace Restfulie.Server.Serializers
{
    public interface ISerializer
    {
        string Serialize(IBehaveAsResource resource);
    }
}
