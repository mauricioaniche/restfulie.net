using System.Collections.Generic;
using System.Xml;
using Restfulie.Server.Request;
using System.Linq;

namespace Restfulie.Server.Marshalling.Serializers.AtomPlusXml
{
    public class AtomPlusXmlHypermediaInserter : IHypermediaInserter
    {
        public string Insert(string content, Relations relations, IRequestInfoFinder requestInfo)
        {
            var xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(content);

            foreach (var state in relations.GetAll())
            {
                var transition = BuildTransition(xmlDocument, state);
                xmlDocument.DocumentElement.AppendChild(transition);
            }

            ReplaceEntryUrl(xmlDocument.DocumentElement, relations);

            return xmlDocument.InnerXml;
        }

        public string Insert(string content, IList<Relations> relations, IRequestInfoFinder requestInfo)
        {
            var xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(content);

            var nodes = xmlDocument.DocumentElement.GetElementsByTagName("entry");
            for (var i = 0; i < nodes.Count; i++)
            {
                var currentNode = nodes[i];
                var currentRelation = relations[i];

                foreach (var relation in currentRelation.GetAll())
                {
                    var transition = BuildTransition(xmlDocument, relation);
                    currentNode.AppendChild(transition);
                }

                ReplaceEntryUrl(currentNode, currentRelation);
            }

            ReplaceFeedUrl(xmlDocument.DocumentElement, requestInfo);

            return xmlDocument.InnerXml;
        }


        private XmlNode BuildTransition(XmlDocument xmlDocument, Relation state)
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

        private void ReplaceEntryUrl(XmlNode node, Relations relations)
        {
            var self = relations.GetAll().Where(r => r.Name.ToLower().Equals("self")).SingleOrDefault();
            if (self != null)
            {
                var id = FindNode(node, "id");
                id.InnerText = self.Url;
            }
        }

        private XmlNode FindNode(XmlNode root, string name)
        {
            for(var i = 0; i < root.ChildNodes.Count; i++)
            {
                if (root.ChildNodes[i].Name.Equals(name)) return root.ChildNodes[i];
            }

            return null;
        }

        private void ReplaceFeedUrl(XmlNode node, IRequestInfoFinder requestInfo)
        {
            var id = FindNode(node, "id");
            id.InnerText = requestInfo.GetUrl();
        }
    }
}
