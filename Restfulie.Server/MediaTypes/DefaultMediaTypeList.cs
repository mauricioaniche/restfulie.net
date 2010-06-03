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

        public IMediaType Find(string name)
        {
            foreach (var mediaType in MediaTypes)
            {
                foreach (var type in mediaType.Synonyms)
                {
                    if (name.Equals(type)) return mediaType;
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
