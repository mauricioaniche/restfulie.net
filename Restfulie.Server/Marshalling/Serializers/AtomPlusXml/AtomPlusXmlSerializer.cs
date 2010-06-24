using System;
using System.Collections;
using System.Xml.Linq;
using Restfulie.Server.Extensions;

// based on http://geekswithblogs.net/lszk/archive/2009/08/23/own-rssatom-feed-in-asp.net-mvc.aspx
namespace Restfulie.Server.Marshalling.Serializers.AtomPlusXml
{
    public class AtomPlusXmlSerializer : IResourceSerializer
    {
        private static readonly XNamespace ns = "http://www.w3.org/2005/Atom";

        public string Serialize(object resource)
        {
            if (resource is IEnumerable)
            {
                var feed = new Feed
                               {
                                   Author = resource.ToString(), 
                                   Description = resource.ToString(), 
                                   Title = resource.ToString(),
                                   Updated = DateTime.Now, 
                                   Id = resource.ToString()
                               };

                foreach (var obj in (IEnumerable)resource)
                {
                    feed.Items.Add(GenerateEntry(obj));
                }

                return FeedInXml(feed).ToString();
            }

            return EntryInXml(GenerateEntry(resource)).ToString();
        }

        private Entry GenerateEntry(object resource)
        {
            var item = new Entry
            {
                Description = resource.ToString(),
                Title = resource.ToString(),
                Id = resource.GetProperty("Id") ?? resource.GetProperty("ID"),
                PublicDate = resource.GetProperty("UpdatedAt") ?? DateTime.Now.ToString(),
                Content = resource.AsXml()
            };
            
            return item;
        }

        private XDocument FeedInXml(Feed atomFeeds)
        {
            var doc = new XDocument(
                new XDeclaration("1.0", "UTF-8", ""),
                new XElement(ns + "feed",
                    new XElement(ns + "title", atomFeeds.Title),
                    new XElement(ns + "updated", atomFeeds.Updated.ToString("yyyy-MM-dd\\THH:mm:ss%K")),
                    new XElement(ns + "author", new XElement(ns + "name", atomFeeds.Author)),
                    new XElement(ns + "id", "id")
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
                                                    item.PublicDate));

            element.Add(new XElement(ns + "content", new XCData(item.Content)));
            return element;
        }
    }
}
