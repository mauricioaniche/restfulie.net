using System.Collections.Generic;
using System.Xml;
using Restfulie.Server.Http;

namespace Restfulie.Server.Marshalling.Serializers.XmlAndHypermedia
{
    public class XmlHypermediaInjector : IHypermediaInjector
    {
        #region IHypermediaInjector Members

        public string Inject(string content, Relations relations, IRequestInfoFinder requestInfo)
        {
            var xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(content);

            foreach (var state in relations.GetAll())
            {
                var transition = GetTransition(xmlDocument, state);

                xmlDocument.DocumentElement.AppendChild(transition);
            }

            return xmlDocument.InnerXml;
        }

        public string Inject(string content, IList<Relations> relations, IRequestInfoFinder requestInfo)
        {
            var xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(content);

            for (var i = 0; i < xmlDocument.DocumentElement.ChildNodes.Count; i++)
            {
                var node = xmlDocument.DocumentElement.ChildNodes[i];

                foreach (var relation in relations[i].GetAll())
                {
                    var transition = GetTransition(xmlDocument, relation);
                    node.AppendChild(transition);
                }
            }

            return xmlDocument.InnerXml;
        }

        #endregion

        private XmlNode GetTransition(XmlDocument xmlDocument, Relation state)
        {
            var transition = xmlDocument.CreateNode(XmlNodeType.Element, "atom", "link", "http://www.w3.org/2005/Atom");

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