using System;
using System.Collections.Generic;
using Restfulie.Server.Exceptions;
using Restfulie.Server.Marshalling.Serializers;

namespace Restfulie.Server.Negotitation
{
    public class AcceptHeaderToSerializer
    {
        private static readonly IDictionary<string, Type> Types;
        private static readonly IDictionary<string, string> Synonims;

        static AcceptHeaderToSerializer()
        {
            Types = new Dictionary<string, Type>
                        {
                            {"xml", typeof (XmlAndHypermediaSerializer)}
                        };

            Synonims = new Dictionary<string, string>
                           {
                               {"application/xml", "xml"}
                           };
        }

        public ISerializer For(string mediaType)
        {
            if (!Synonims.ContainsKey(mediaType)) throw new MediaTypeNotSupportedException();

            return (ISerializer)Activator.CreateInstance(Types[Synonims[mediaType]]);
        }
    }
}
