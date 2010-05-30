using System;
using System.Collections.Generic;
using Restfulie.Server.Exceptions;
using Restfulie.Server.Marshalling.Serializers;
using Restfulie.Server.Marshalling.Serializers.AtomPlusXml;

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
                                 {"application/xml", typeof (XmlAndHypermediaSerializer)},
                                 {"text/xml", typeof (XmlAndHypermediaSerializer)},
                                 {"xml", typeof (XmlAndHypermediaSerializer)},
                                 {"application/atom", typeof (AtomPlusXmlSerializer)},
                                 {"application/atom+xml", typeof (AtomPlusXmlSerializer)},
                                 {"atom", typeof (AtomPlusXmlSerializer)}
                             };

            DefaultSerializer = typeof (XmlAndHypermediaSerializer);
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
            return (IResourceSerializer)Activator.CreateInstance(
                                            string.IsNullOrEmpty(mediaType) ? DefaultSerializer : SearchFor(mediaType));
        }
    }
}