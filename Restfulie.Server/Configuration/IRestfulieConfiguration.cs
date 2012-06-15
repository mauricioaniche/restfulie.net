using Restfulie.Server.MediaTypes;

namespace Restfulie.Server.Configuration
{
    public interface IRestfulieConfiguration
    {
        IMediaTypeList MediaTypeList { get; }
        void Register<T>(IDriver driver) where T : IMediaType;
        void RegisterVendorized(string format, IDriver driver);
        void Remove<T>() where T : IMediaType;
        void Remove(IMediaType mediaTypeToRemove);
        void SetDefaultMediaType<TDefault>() where TDefault : IMediaType;
    }
}