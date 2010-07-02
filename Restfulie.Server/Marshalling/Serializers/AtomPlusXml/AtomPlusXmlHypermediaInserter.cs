using System.Collections.Generic;
using System.Xml;
using Restfulie.Server.Request;
using System.Linq;

namespace Restfulie.Server.Marshalling.Serializers.AtomPlusXml
{
    public class AtomPlusXmlHypermediaInserter : IHypermediaInserter
    {
        public string Insert(string content, Relations relations, IRequestInfoFinder finder)
        {
            var xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(content);

            foreach (var state in relations.GetAll())
            {
                XmlNode transition = GetTransition(xmlDocument, state);

                xmlDocument.DocumentElement.AppendChild(transition);
            }

            var xml = xmlDocument.InnerXml;

            var getRelation = relations.GetAll().Where(r => r.Name.ToLower().Equals("get")).SingleOrDefault();
            if (getRelation != null)
            {
                xml = xml.Replace("(entry-url)", getRelation.Url);
            }
            return xml;
        }

        public string Insert(string content, IList<Relations> relations, IRequestInfoFinder finder)
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
