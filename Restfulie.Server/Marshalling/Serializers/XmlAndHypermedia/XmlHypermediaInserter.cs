using System;
using System.Collections.Generic;
using System.Xml;

namespace Restfulie.Server.Marshalling.Serializers.XmlAndHypermedia
{
    public class XmlHypermediaInserter : IHypermediaInserter
    {
        public string Insert(string content, IList<Relation> relations)
        {
            var xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(content);

            foreach (var state in relations)
            {
                XmlNode transition = GetTransition(xmlDocument, state);

                xmlDocument.DocumentElement.AppendChild(transition);
            }

            return xmlDocument.InnerXml;
        }

        public string Insert(string content, IList<IList<Relation>> relations)
        {
            var xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(content);

            for (var i = 0; i < xmlDocument.DocumentElement.ChildNodes.Count; i++)
            {
                var node = xmlDocument.DocumentElement.ChildNodes[i];

                foreach(var relation in relations[i])
                {
                    var transition = GetTransition(xmlDocument, relation);
                    node.AppendChild(transition);
                }
            }

            return xmlDocument.InnerXml;
        }


        private XmlNode GetTransition(XmlDocument xmlDocument, Relation state)
        {
            var transition = xmlDocument.CreateNode(XmlNodeType.Element, "atom", "link", "http://www.w3.org/2005/Atom");
            var rel = xmlDocument.CreateAttribute("rel");
            transition.InnerText = state.Url;
            rel.InnerText = state.Name;
            transition.Attributes.Append(rel);
            return transition;
        }

    }
}
