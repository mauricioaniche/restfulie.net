using System;
using System.Web.Mvc;
using Restfulie.Server.Marshalling;
using Restfulie.Server.Unmarshalling;

namespace Restfulie.Server.MediaTypes
{
    class HTML : IMediaType
    {
        public string Name
        {
            get { return "application/html"; }
        }

        public string[] Acronyms
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
                throw new NotImplementedException();
            }
        }
    }
}
