using System;
using Restfulie.Server.MediaTypes;

namespace Restfulie.Server.Configuration
{
    public interface IRestfulieConfiguration
    {
        void Register<T>(IDriver driver) where T : IMediaType;
        void RegisterVendorized(string format, IDriver driver);
        IMediaTypeList MediaTypes { get; }
    	void Remove<T>() where T : IMediaType;
    	void Remove<TMediaTypeToRemove>(IMediaType defaultMediaType) where TMediaTypeToRemove : IMediaType;
    	void Remove<TMediaTypeToRemove>(Type defaultMediaType) where TMediaTypeToRemove : IMediaType;
    }
}