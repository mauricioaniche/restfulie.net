using System.Collections.Generic;
using Restfulie.Server.Exceptions;
using Restfulie.Server.MediaTypes;

namespace Restfulie.Server.Negotiation
{
    public class DefaultContentNegotiation : IContentNegotiation
    {
        private static readonly IList<IMediaType> MediaTypes;

        static DefaultContentNegotiation()
        {
            MediaTypes = new List<IMediaType>
                             {
                                 new XmlAndHypermedia(), 
                                 new HTML()
                             };
        }

        public IMediaType ForRequest(string mediaType)
        {
            foreach (var type in MediaTypes)
            {
                foreach(var name in type.Acronyms)
                {
                    if (name.Contains(mediaType)) return type;
                }
            }

            throw new MediaTypeNotSupportedException();
        }

        public IMediaType ForResponse(string mediaType)
        {
            foreach (var type in MediaTypes)
            {
                foreach (var name in type.Acronyms)
                {
                    if (name.Contains(mediaType)) return type;
                }
            }

            throw new ContentTypeNotSupportedException();
        }
    }
}
