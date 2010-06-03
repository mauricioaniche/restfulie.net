using System;
using System.Web.Mvc;
using Restfulie.Server.Marshalling;
using Restfulie.Server.Unmarshalling;

namespace Restfulie.Server.MediaTypes
{
    class HTML : IMediaType
    {
        public string FriendlyName
        {
            get { return "HTML"; }
        }

        public string[] Synonyms
        {
            get { return new[] {"application/html"}; }
        }

        public IResourceMarshaller Marshaller
        {
            get
            {
                return new AspNetMvcMarshaller(new ViewResult());
            }
        }

        public IResourceUnmarshaller Unmarshaller
        {
            get
            {
                return new AspNetMvcUnmarshaller();
            }
        }
    }
}
