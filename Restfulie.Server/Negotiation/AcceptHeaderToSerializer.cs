using System;
using System.Collections.Generic;
using Restfulie.Server.Exceptions;
using Restfulie.Server.Marshalling.Serializers;
using Restfulie.Server.Marshalling.Serializers.AtomPlusXml;
using Restfulie.Server.Marshalling.Serializers.XmlAndHypermedia;

namespace Restfulie.Server.Negotiation
{
    public class AcceptHeaderToSerializer
    {
        private static readonly IDictionary<string, Type> MediaTypes;
        private static readonly Type DefaultSerializer;

        static AcceptHeaderToSerializer()
        {
            MediaTypes = new Dictionary<string, Type>
                             {
                                 {"application/xml", typeof (XmlAndHypermediaSerializerFactory)},
                                 {"text/xml", typeof (XmlAndHypermediaSerializerFactory)},
                                 {"xml", typeof (XmlAndHypermediaSerializerFactory)},
                                 {"application/atom", typeof (AtomPlusXmlSerializerFactory)},
                                 {"application/atom+xml", typeof (AtomPlusXmlSerializerFactory)},
                                 {"atom", typeof (AtomPlusXmlSerializerFactory)}
                             };

            DefaultSerializer = typeof(XmlAndHypermediaSerializerFactory);
        }

        private Type SearchFor(string expression)
        {
            foreach(var mediaType in MediaTypes)
            {
                if (expression.Contains(mediaType.Key)) return mediaType.Value;
            }

            throw new MediaTypeNotSupportedException();
        }

        public IResourceSerializer For(string mediaType)
        {
            var factory = (ISerializerFactory) Activator.CreateInstance(
                                            string.IsNullOrEmpty(mediaType) ? DefaultSerializer : SearchFor(mediaType));

            return factory.Create();
        }
    }
}