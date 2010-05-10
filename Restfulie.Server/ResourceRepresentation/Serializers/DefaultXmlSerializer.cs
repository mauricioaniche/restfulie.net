using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace Restfulie.Server.ResourceRepresentation.Serializers
{
    public class DefaultXmlSerializer : ISerializer
    {
        public string Serialize(IBehaveAsResource resource, IList<Transition> transitions)
        {
            return PutTransitionsOn(GetXmlBasedOn(resource), transitions);
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

        private string PutTransitionsOn(XmlDocument xmlDocument, IList<Transition> transitions)
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

            return xmlDocument.InnerXml;
        }
    }
}