using Restfulie.Server.MediaTypes;

namespace Restfulie.Server.Negotiation
{
    public interface IContentNegotiation
    {
        IMediaType ForRequest(string mediaType);
        IMediaType ForResponse(string mediaType);
    }
}
