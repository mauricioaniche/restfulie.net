using System.Collections.Generic;

namespace Restfulie.Server.MediaTypes
{
    class DefaultMediaTypeList : IMediaTypeList
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
    }
}
