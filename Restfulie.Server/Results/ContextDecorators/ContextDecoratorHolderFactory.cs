using Restfulie.Server.MediaTypes;

namespace Restfulie.Server.Results.ContextDecorators
{
    public class ContextDecoratorHolderFactory
    {
        public IContextDecoratorHolder BasedOn(IMediaType mediaType)
        {
            return (mediaType is HTML) ?  (IContextDecoratorHolder) 
                new AspNetMvcDecoratorHolder() :
                new RestfulieDecoratorHolder();
        }
    }
}
