using System;
using System.Collections.Generic;
using Restfulie.Server.Exceptions;
using Restfulie.Server.Unmarshalling.Deserializers;
using Restfulie.Server.Unmarshalling.Deserializers.Xml;

namespace Restfulie.Server.Negotiation
{
    public class ContentTypeToDeserializer
    {
        private static readonly IDictionary<string, Type> MediaTypes;
        private static readonly Type DefaultSerializer;

        static ContentTypeToDeserializer()
        {
            MediaTypes = new Dictionary<string, Type>
                             {
                                 {"application/xml", typeof (XmlDeserializerFactory)},
                                 {"text/xml", typeof (XmlDeserializerFactory)},
                                 {"xml", typeof (XmlDeserializerFactory)}
                             };

            DefaultSerializer = typeof(XmlDeserializerFactory);
        }

        private Type SearchFor(string expression)
        {
            foreach(var mediaType in MediaTypes)
            {
                if (expression.Contains(mediaType.Key)) return mediaType.Value;
            }

            throw new ContentTypeNotSupportedException();
        }

        public IResourceDeserializer For(string mediaType)
        {
            var factory = (IDeserializerFactory)Activator.CreateInstance(
                                              string.IsNullOrEmpty(mediaType) ? DefaultSerializer : SearchFor(mediaType));

            return factory.Create();
        }
    }
}