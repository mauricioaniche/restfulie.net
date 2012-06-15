using Restfulie.Server.MediaTypes;

namespace Restfulie.Server.Negotiation
{
    public class ContentTypeToMediaType : IContentTypeToMediaType
    {
        private readonly IMediaTypeList mediaTypes;

        public ContentTypeToMediaType(IMediaTypeList mediaTypes)
        {
            this.mediaTypes = mediaTypes;
        }

        #region IContentTypeToMediaType Members

        public IMediaType GetMediaType(string contentType)
        {
            var mediaType = mediaTypes.Find(contentType);
            if (mediaType == null)
                throw new ContentTypeNotSupportedException();
            return mediaType;
        }

        #endregion
    }
}