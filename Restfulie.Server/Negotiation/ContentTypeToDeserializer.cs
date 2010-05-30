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
                                 {"application/xml", typeof (XmlDeserializer)},
                                 {"text/xml", typeof (XmlDeserializer)},
                                 {"xml", typeof (XmlDeserializer)}
                             };

            DefaultSerializer = typeof (XmlDeserializer);
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
            return (IResourceDeserializer)Activator.CreateInstance(
                                              string.IsNullOrEmpty(mediaType) ? DefaultSerializer : SearchFor(mediaType));
        }
    }
}