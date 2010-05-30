using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using System.Linq;

namespace Restfulie.Server.Marshalling.Serializers.XmlAndHypermedia
{
    public class XmlAndHypermediaSerializer : IResourceSerializer
    {
        public string Serialize(IBehaveAsResource resource, IList<Relation> transitions)
        {
            return PutTransitionsOn(GetXmlBasedOn(resource), transitions).InnerXml;
        }

        public string Serialize(IDictionary<IBehaveAsResource, IList<Relation>> resources, string rootName)
        {
            var resourcesInXml = new StringBuilder();

            foreach(var resource in resources)
            {
                resourcesInXml.Append(PutTransitionsOn(GetXmlBasedOn(resource.Key), resource.Value).DocumentElement.OuterXml);
            }

            var xmlDocument = new XmlDocument();
            xmlDocument.LoadXml("<" + rootName + ">" + resourcesInXml + "</" + rootName + ">");
           
            return xmlDocument.InnerXml;
        }

        public string Format
        {
            get { return "application/xml"; }
        }

        private XmlDocument GetXmlBasedOn(IBehaveAsResource resource)
        {
            var stream = new MemoryStream();
            var s = new XmlSerializer(resource.GetType());
            s.Serialize(stream, resource);
            stream.Seek(0, SeekOrigin.Begin);

            var xmlDocument = new XmlDocument();
            xmlDocument.Load(stream);

            stream.Close();
            return xmlDocument;
        }

        private XmlDocument PutTransitionsOn(XmlDocument xmlDocument, IList<Relation> transitions)
        {
            foreach (var state in transitions)
            {
                var transition = xmlDocument.CreateNode(XmlNodeType.Element, "atom", "link", "http://www.w3.org/2005/Atom");
                var rel = xmlDocument.CreateAttribute("rel");
                transition.InnerText = state.Url;
                rel.InnerText = state.Name;
                transition.Attributes.Append(rel);

                xmlDocument.DocumentElement.AppendChild(transition);
            }

            return xmlDocument;
        }
    }
}