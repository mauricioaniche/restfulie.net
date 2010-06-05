using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace Restfulie.Server.Extensions
{
    public static class ObjectExtensions
    {
        public static IBehaveAsResource[] AsResourceArray(this object obj)
        {
            var resources = new List<IBehaveAsResource>();

            foreach(var item in (IEnumerable)obj)
            {
                resources.Add((IBehaveAsResource)item);
            }

            return resources.ToArray();
        }

        public static string AsXml(this object resource)
        {
            var stringWriter = new StringWriter();
            using (var xmlWriter = XmlWriter.Create(stringWriter, new XmlWriterSettings { OmitXmlDeclaration = true }))
            {
                var noNamespaces = new XmlSerializerNamespaces();
                noNamespaces.Add("", "");
                new XmlSerializer(resource.GetType()).Serialize(xmlWriter, resource, noNamespaces);
            }

            return stringWriter.ToString();
        }
    }
}
