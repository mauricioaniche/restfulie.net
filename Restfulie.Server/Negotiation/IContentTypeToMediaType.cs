using Restfulie.Server.MediaTypes;

namespace Restfulie.Server.Negotiation
{
    public interface IContentTypeToMediaType
    {
        IMediaType GetMediaType(string acceptHeader);
    }
}