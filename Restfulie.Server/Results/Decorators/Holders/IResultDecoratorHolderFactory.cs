using Restfulie.Server.MediaTypes;

namespace Restfulie.Server.Results.Decorators.Holders
{
    public interface IResultDecoratorHolderFactory
    {
        IResultDecoratorHolder BasedOn(IMediaType mediaType);
    }
}
