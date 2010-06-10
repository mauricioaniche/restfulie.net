using System.Collections.Generic;

namespace Restfulie.Server.MediaTypes
{
    public class DefaultMediaTypeList : IMediaTypeList
    {
        public IEnumerable<IMediaType> MediaTypes { get; private set; }

        public DefaultMediaTypeList(IEnumerable<IMediaType> mediaTypes)
        {
            MediaTypes = mediaTypes;
        }

        public IMediaType Find(string format)
        {
            foreach (var mediaType in MediaTypes)
            {
                foreach (var type in mediaType.Synonyms)
                {
                    if (format.Equals(type)) return mediaType;
                }
            }

            return null;
        }

        public IMediaType Default
        {
            get { return new HTML(); }
        }
    }
}
