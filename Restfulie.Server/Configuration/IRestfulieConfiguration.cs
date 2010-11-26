using Restfulie.Server.MediaTypes;

namespace Restfulie.Server.Configuration
{
    public interface IRestfulieConfiguration
    {
        void Register<T>(IDriver driver) where T : IMediaType;
        void RegisterVendorized(string format, IDriver driver);
        IMediaTypeList MediaTypeList { get; }
    	void Remove<T>() where T : IMediaType;
    	void Remove(IMediaType mediaTypeToRemove);
    	void SetDefaultMediaType<TDefault>() where TDefault : IMediaType;
    }
}