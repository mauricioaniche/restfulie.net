using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

// based on http://geekswithblogs.net/lszk/archive/2009/08/23/own-rssatom-feed-in-asp.net-mvc.aspx
namespace Restfulie.Server.Marshalling.Serializers.AtomPlusXml
{
    public class AtomPlusXmlSerializer : IResourceSerializer
    {
        private static readonly XNamespace ns = "http://www.w3.org/2005/Atom";

        public string Serialize(IBehaveAsResource resource, IList<Relation> transitions)
        {
            var item = GenerateEntry(resource, transitions);
            return EntryInXml(item).ToString();
        }

        public string Serialize(IDictionary<IBehaveAsResource, IList<Relation>> resources)
        {
            var feed = new Feed {Author = "", Description = "", Title = "", Updated = DateTime.Now, Id = ""};
            foreach (var resource in resources) 
            {
                feed.Items.Add(GenerateEntry(resource.Key, resource.Value));
            }

            return FeedInXml(feed).ToString();

        }

        public string Format
        {
            get { return "application/atom+xml"; }
        }

        private Entry GenerateEntry(IBehaveAsResource resource, IList<Relation> transitions)
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
                item.Links.Add(new Link { Rel = relation.Name, HRef = relation.Url });
            }
            return item;
        }

        private string SerializeResource(IBehaveAsResource resource)
        {
            var writerSettings = new XmlWriterSettings {OmitXmlDeclaration = true};
            var stringWriter = new StringWriter();
            using (var xmlWriter = XmlWriter.Create(stringWriter, writerSettings))
            {
                var noNamespaces = new XmlSerializerNamespaces();
                noNamespaces.Add("", "");
                new XmlSerializer(resource.GetType()).Serialize(xmlWriter, resource, noNamespaces);
            }

            return stringWriter.ToString();
        }

        private XDocument FeedInXml(Feed atomFeeds)
        {
            var doc = new XDocument(
                new XDeclaration("1.0", "UTF-8", ""),
                new XElement(ns + "feed",
                    new XElement(ns + "title", atomFeeds.Title),
                    new XElement(ns + "updated", atomFeeds.Updated.ToString("yyyy-MM-dd\\THH:mm:ss%K")),
                    new XElement(ns + "author", new XElement(ns + "name", atomFeeds.Author)),
                    new XElement(ns + "id", "http://localhost:3563/MyAtomFeedId")
                    ));

            foreach (var item in atomFeeds.Items)
            {
                doc.Element(ns + "feed").Add(EntryInXml(item));
            }

            return doc;
        }

        private XElement EntryInXml(Entry item)
        {
            var element = new XElement(ns + "entry",
                                       new XElement(ns + "title", item.Title),
                                       new XElement(ns + "id", item.Id),
                                       new XElement(ns + "updated",
                                                    item.PublicDate.ToString("yyyy-MM-dd\\THH:mm:ss%K")));

            foreach (var link in item.Links)
            {
                element.Add(new XElement(ns + "link",
                                         new XAttribute("rel", link.Rel),
                                         new XAttribute("href", link.HRef)));
            }

            element.Add(new XElement(ns + "content", new XCData(item.Content)));
            return element;
        }
    }
}
