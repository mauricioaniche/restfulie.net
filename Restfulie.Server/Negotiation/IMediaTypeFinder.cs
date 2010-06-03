using Restfulie.Server.MediaTypes;

namespace Restfulie.Server.Negotiation
{
    public interface IMediaTypeFinder
    {
        IMediaType GetMediaType(string acceptHeader);
    }
}
