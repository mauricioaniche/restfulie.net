using System;
using System.Collections.Generic;
using Restfulie.Server.Exceptions;
using Restfulie.Server.Marshalling.Serializers;

namespace Restfulie.Server.Negotitation
{
    public class AcceptHeaderToSerializer
    {
        private static readonly IDictionary<string, Type> MediaTypes;
        private static readonly Type DefaultMediaType;

        static AcceptHeaderToSerializer()
        {
            MediaTypes = new Dictionary<string, Type>
                           {
                               {"application/xml", typeof (XmlAndHypermediaSerializer)}
                           };

            DefaultMediaType = typeof (XmlAndHypermediaSerializer);
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
                string.IsNullOrEmpty(mediaType) ? DefaultMediaType : SearchFor(mediaType));
        }
    }
}
