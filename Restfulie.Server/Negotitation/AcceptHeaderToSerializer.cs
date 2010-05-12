using System;
using System.Collections.Generic;
using Restfulie.Server.Marshalling.Serializers;

namespace Restfulie.Server.Negotitation
{
    public class AcceptHeaderToSerializer
    {
        private static IDictionary<string, Type> types;
        private static IDictionary<string, string> synonims;

        static AcceptHeaderToSerializer()
        {
            types = new Dictionary<string, Type>
                        {
                            {"xml", typeof (XmlAndHypermediaSerializer)}
                        };

            synonims = new Dictionary<string, string>
                           {
                               {"application/xml", "xml"}
                           };
        }

        public ISerializer For(string mimeType)
        {
            return (ISerializer)Activator.CreateInstance(types[synonims[mimeType]]);
        }
    }
}
