using Restfulie.Server.MediaTypes;

namespace Restfulie.Server.Configuration
{
    public interface IRestfulieConfiguration
    {
        void Register<T>(IDriver driver) where T : IMediaType;
        void RegisterVendorized(string format, IDriver driver);
        IMediaTypeList MediaTypes { get; }
    }
}