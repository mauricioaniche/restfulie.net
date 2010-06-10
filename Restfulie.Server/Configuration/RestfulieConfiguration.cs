using System;
using System.Collections.Generic;
using Restfulie.Server.Marshalling.Serializers;
using Restfulie.Server.MediaTypes;
using Restfulie.Server.Unmarshalling.Deserializers;
using System.Linq;

namespace Restfulie.Server.Configuration
{
    public class RestfulieConfiguration : IRestfulieConfiguration
    {
        private readonly IList<IMediaType> store;

        public RestfulieConfiguration()
        {
            store = new List<IMediaType>
                        {
                            new XmlAndHypermedia(),
                            new AtomPlusXml(),
                            new HTML()
                        };
        }

        public void Register<T>(IResourceSerializer serializer, IResourceDeserializer deserializer) 
            where T : IMediaType 
        {
            var media = FindOrCreate(typeof (T));
            media.Serializer = serializer;
            media.Deserializer = deserializer;
        }

        public IMediaTypeList MediaTypes
        {
            get { return new DefaultMediaTypeList(store); }
        }

        private IMediaType FindOrCreate(Type mediaType)
        {
            var searchedMediaType = store.Where(mt => mt.GetType() == mediaType).SingleOrDefault();
            if(searchedMediaType == null)
            {
                searchedMediaType = (IMediaType)Activator.CreateInstance(mediaType);
                store.Add(searchedMediaType);
            }

            return searchedMediaType;
        }
    }
}
