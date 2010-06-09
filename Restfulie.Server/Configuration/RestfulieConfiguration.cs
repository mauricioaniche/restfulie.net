using System;
using System.Collections.Generic;
using Restfulie.Server.Marshalling.Serializers;
using Restfulie.Server.MediaTypes;
using Restfulie.Server.Unmarshalling.Deserializers;
using System.Linq;

namespace Restfulie.Server.Configuration
{
    public class RestfulieConfiguration : IConfiguration
    {
        private static readonly IList<MediaTypeSerializerAndDeserializer> Store;

        static RestfulieConfiguration()
        {
            Store = new List<MediaTypeSerializerAndDeserializer>();
        }

        public void Register<T, T1, T2>() 
            where T : IMediaType 
            where T1 : IResourceSerializer 
            where T2 : IResourceDeserializer
        {
            Store.Add(new MediaTypeSerializerAndDeserializer(typeof(T), typeof(T1), typeof(T2)));
        }

        public IResourceSerializer GetSerializer<T>() where T : IMediaType
        {
            var item = FindMediaType<T>();
            return item == null ? null : (IResourceSerializer)Activator.CreateInstance(item.Serializer);
        }

        public IResourceDeserializer GetDeserializer<T>() where T : IMediaType
        {
            var item = FindMediaType<T>();
            return item == null ? null : (IResourceDeserializer)Activator.CreateInstance(item.Deserializer);
        }

        public void ClearDefaults()
        {
            Store.Clear();
        }

        private MediaTypeSerializerAndDeserializer FindMediaType<T>()
        {
            return Store.Where(mt => mt.MediaType == typeof(T)).FirstOrDefault();
        }

    }
}
