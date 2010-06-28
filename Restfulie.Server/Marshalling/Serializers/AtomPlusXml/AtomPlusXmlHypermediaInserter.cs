using System;
using System.Collections.Generic;
using System.Xml;

namespace Restfulie.Server.Marshalling.Serializers.AtomPlusXml
{
    public class AtomPlusXmlHypermediaInserter : IHypermediaInserter
    {
        public string Insert(string content, Relations relations)
        {
            var xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(content);

            foreach (var state in relations.GetAll())
            {
                XmlNode transition = GetTransition(xmlDocument, state);

                xmlDocument.DocumentElement.AppendChild(transition);
            }

            return xmlDocument.InnerXml;
        }

        public string Insert(string content, IList<Relations> relations)
        {
            var xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(content);

            var nodes = xmlDocument.DocumentElement.GetElementsByTagName("entry");

            for (var i = 0; i < nodes.Count; i++)
            {
                var node = nodes[i];

                foreach (var relation in relations[i].GetAll())
                {
                    var transition = GetTransition(xmlDocument, relation);
                    node.AppendChild(transition);
                }
            }

            return xmlDocument.InnerXml;
        }


        private XmlNode GetTransition(XmlDocument xmlDocument, Relation state)
        {
            var transition = xmlDocument.CreateNode(XmlNodeType.Element, "link", "");

            var rel = xmlDocument.CreateAttribute("rel");
            rel.InnerText = state.Name;
            transition.Attributes.Append(rel);

            var href = xmlDocument.CreateAttribute("href");
            href.InnerText = state.Url;
            transition.Attributes.Append(href);

            return transition;
        }
    }
}
