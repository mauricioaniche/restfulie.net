using Restfulie.Server.MediaTypes;

namespace Restfulie.Server.Negotiation
{
    public interface IAcceptHeaderToMediaType
    {
        IMediaType GetMediaType(string acceptHeader);
    }
}