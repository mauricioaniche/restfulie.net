using Restfulie.Server.MediaTypes;

namespace Restfulie.Server.Negotiation
{
    public class ContentTypeToMediaType
    {
        private readonly IMediaTypeList mediaTypes;

        public ContentTypeToMediaType(IMediaTypeList mediaTypes)
        {
            this.mediaTypes = mediaTypes;
        }

        public IMediaType GetMediaType(string contentType)
        {
            var mediaType = mediaTypes.Find(contentType);
            if(mediaType == null) throw new ResponseMediaTypeNotSupportedException();
            return mediaType;
        }
    }
}
