using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using System.Linq;
using Restfulie.Server.Extensions;

namespace Restfulie.Server.Marshalling.Serializers.XmlAndHypermedia
{
    public class XmlAndHypermediaSerializer : IResourceSerializer
    {
        private readonly IInflections inflections;

        public XmlAndHypermediaSerializer(IInflections inflections)
        {
            this.inflections = inflections;
        }

        public string Serialize(IBehaveAsResource resource, IList<Relation> transitions)
        {
            return PutTransitionsOn(GetXmlBasedOn(resource), transitions).InnerXml;
        }

        public string Serialize(IDictionary<IBehaveAsResource, IList<Relation>> resources)
        {
            var resourcesInXml = new StringBuilder();

            foreach(var resource in resources)
            {
                resourcesInXml.Append(PutTransitionsOn(GetXmlBasedOn(resource.Key), resource.Value).DocumentElement.OuterXml);
            }

            var rootName = inflections.Inflect(resources.First().Key.GetType().Name);
            var xmlDocument = new XmlDocument();
            xmlDocument.LoadXml("<" + rootName + ">" + resourcesInXml + "</" + rootName + ">");
           
            return xmlDocument.InnerXml;
        }

        private XmlDocument GetXmlBasedOn(IBehaveAsResource resource)
        {
            var writerSettings = new XmlWriterSettings { OmitXmlDeclaration = true };
            var stringWriter = new StringWriter();
            using (var xmlWriter = XmlWriter.Create(stringWriter, writerSettings))
            {
                var noNamespaces = new XmlSerializerNamespaces();
                noNamespaces.Add("", "");
                new XmlSerializer(resource.GetType()).Serialize(xmlWriter, resource, noNamespaces);
            }

            var xmlDocument = new XmlDocument();
            xmlDocument.Load(stringWriter.ToString().AsStream());

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