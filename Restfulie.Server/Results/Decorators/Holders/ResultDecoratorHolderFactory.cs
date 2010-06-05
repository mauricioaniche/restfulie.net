using Restfulie.Server.MediaTypes;

namespace Restfulie.Server.Results.Decorators.Holders
{
    public class ResultDecoratorHolderFactory : IResultDecoratorHolderFactory
    {
        public IResultDecoratorHolder BasedOn(IMediaType mediaType)
        {
            return (mediaType is HTML) ?  (IResultDecoratorHolder) 
                                          new AspNetMvcResultHolder() :
                                          new RestfulieResultHolder();
        }
    }
}