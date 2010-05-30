using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Linq;
using System.Xml.Serialization;

// based on http://geekswithblogs.net/lszk/archive/2009/08/23/own-rssatom-feed-in-asp.net-mvc.aspx
namespace Restfulie.Server.Marshalling.Serializers.AtomPlusXml
{
    public class AtomPlusXmlSerializer : IResourceSerializer
    {
        private const string ns = "http://www.w3.org/2005/Atom";

        public string Serialize(IBehaveAsResource resource, IList<Relation> transitions)
        {
            var item = new Entry
                           {
                               Description = resource.ToString(),
                               Title = resource.ToString(),
                               Id = resource.ToString(),
                               PublicDate = DateTime.Now,
                               Content = SerializeResource(resource)
                           };
            foreach (var relation in transitions)
            {
                item.Links.Add(new Link { Rel = relation.Name, HRef = relation.Url});
            }

            return SerializeEntry(item).ToString();
        }

        public string Serialize(IDictionary<IBehaveAsResource, IList<Relation>> resources)
        {
            throw new NotImplementedException();
        }

        public string Format
        {
            get { return "application/atom+xml"; }
        }

        private string SerializeResource(IBehaveAsResource resource)
        {
            var stream = new MemoryStream();
            new XmlSerializer(resource.GetType()).Serialize(stream, resource);

            stream.Seek(0, SeekOrigin.Begin);
            return new StreamReader(stream).ReadToEnd();
        }

        private XDocument CreateXmlDoc(Feed atomFeeds)
        {
            var doc = new XDocument(
                new XDeclaration("1.0", "UTF-8", ""),
                new XElement(ns + "feed",
                    new XElement(ns + "title", atomFeeds.Title),
                    new XElement(ns + "link",
                        new XAttribute("href", "http://localhost:3563/Feed/Atom"),
                        new XAttribute("rel", "self")),
                    new XElement(ns + "updated",
                //updated element must be in
                //year-month-dayThour:minuts:secondsTimeZone format
                        atomFeeds.Updated.ToString("yyyy-MM-dd\\THH:mm:ss%K")),
                    new XElement(ns + "author",
                        new XElement(ns + "name", atomFeeds.Author)),
                //id must be constant and unique for this channel
                //if uri address is used, it hasn't be real
                    new XElement(ns + "id", "http://localhost:3563/MyAtomFeedId")
                    ));

            foreach (var item in atomFeeds.Items)
            {
                doc.Element(ns + "feed").Add(SerializeEntry(item));
            }

            return doc;
        }

        private XElement SerializeEntry(Entry item)
        {
            var element = new XElement("entry",
                                       new XElement("title", item.Title),
                                       //id must be constant and unique, otherwise each update
                                       //by feed readers will be duplicating all entries
                                       new XElement("id", item.Id),
                                       new XElement("updated",
                                                    item.PublicDate.ToString("yyyy-MM-dd\\THH:mm:ss%K")));

            foreach (var link in item.Links)
            {
                element.Add(new XElement("link",
                                         new XAttribute("rel", link.Rel),
                                         new XAttribute("href", link.HRef)));
            }
            return element;
        }
    }
}
