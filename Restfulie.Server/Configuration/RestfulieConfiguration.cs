using System;
using System.Collections.Generic;
using Restfulie.Server.Marshalling.Serializers;
using Restfulie.Server.MediaTypes;
using Restfulie.Server.Unmarshalling.Deserializers;
using System.Linq;

namespace Restfulie.Server.Configuration
{
    public class RestfulieConfiguration
    {
        private static IList<MediaTypeSerializerAndDeserializer> store;

        static RestfulieConfiguration()
        {
            store = new List<MediaTypeSerializerAndDeserializer>();
        }

        public void Register<T, T1, T2>() 
            where T : IMediaType 
            where T1 : IResourceSerializer 
            where T2 : IResourceDeserializer
        {
            store.Add(new MediaTypeSerializerAndDeserializer(typeof(T), typeof(T1), typeof(T2)));
        }

        public IResourceSerializer GetSerializer<T>()
        {
            return (IResourceSerializer)
                Activator.CreateInstance(store.Where(mt => mt.MediaType == typeof(T)).First().Serializer);
        }

        public IResourceDeserializer GetDeserializer<T>()
        {
            return (IResourceDeserializer)
                Activator.CreateInstance(store.Where(mt => mt.MediaType == typeof(T)).First().Deserializer);
        }
    }

    class MediaTypeSerializerAndDeserializer
    {
        public Type Serializer { get; private set; }
        public Type Deserializer { get; private set; }
        public Type MediaType { get; private set; }

        public MediaTypeSerializerAndDeserializer(Type mediaType, Type serializer, Type deserializer)
        {
            MediaType = mediaType;
            Serializer = serializer;
            Deserializer = deserializer;
        }
    }
}
