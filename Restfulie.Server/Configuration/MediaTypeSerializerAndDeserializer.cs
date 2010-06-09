using System;

namespace Restfulie.Server.Configuration
{
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
