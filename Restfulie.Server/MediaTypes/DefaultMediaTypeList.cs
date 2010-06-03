using System;
using System.Collections.Generic;

namespace Restfulie.Server.MediaTypes
{
    public class DefaultMediaTypeList : IMediaTypeList
    {
        public IEnumerable<IMediaType> MediaTypes
        {
            get { 
                return new IMediaType[]
                {
                    new HTML(), 
                    new XmlAndHypermedia(), 
                    new AtomPlusXml()
                }; 
            }
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
