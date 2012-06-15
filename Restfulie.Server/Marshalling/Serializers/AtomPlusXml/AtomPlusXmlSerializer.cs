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
        private static readonly XNamespace contentNs = "";

        #region IResourceSerializer Members

        public string Serialize(object resource)
        {
            if (resource is IEnumerable)
            {
                var feed = new Feed
                {
                    Author = "(author)",
                    Description = "(description)",
                    Title = "(title)",
                    Updated = DateTime.Now.ToRFC3339(),
                    Id = "(feed-url)"
                };

                foreach (var obj in (IEnumerable) resource)
                    feed.Items.Add(GenerateEntry(obj));

                return FeedInXml(feed).ToString();
            }

            return EntryInXml(GenerateEntry(resource)).ToString();
        }

        #endregion

        private Entry GenerateEntry(object resource)
        {
            var item = new Entry
            {
                Title = "(title)",
                Id = "(entry-url)",
                PublicDate = GetUpdatedAt(resource),
                Content = resource.AsXml()
            };

            return item;
        }

        private string GetUpdatedAt(object resource)
        {
            var updatedAt = resource.GetProperty("UpdatedAt");
            DateTime date;

            if (updatedAt != null && DateTime.TryParse(updatedAt.ToString(), out date))
                return date.ToRFC3339();

            return DateTime.Now.ToRFC3339();
        }

        private XDocument FeedInXml(Feed atomFeeds)
        {
            var doc = new XDocument(
                new XDeclaration("1.0", "UTF-8", ""),
                new XElement(ns + "feed",
                             new XElement(ns + "title", atomFeeds.Title),
                             new XElement(ns + "updated", atomFeeds.Updated),
                             new XElement(ns + "author", new XElement(ns + "name", atomFeeds.Author)),
                             new XElement(ns + "id", atomFeeds.Id)
                    ));

            foreach (var item in atomFeeds.Items)
                doc.Element(ns + "feed").Add(EntryInXml(item));

            return doc;
        }

        private XElement EntryInXml(Entry item)
        {
            var element = new XElement(ns + "entry",
                                       new XElement(ns + "title", item.Title),
                                       new XElement(ns + "id", item.Id),
                                       new XElement(ns + "updated",
                                                    item.PublicDate));

            element.Add(new XElement(contentNs + "content",
                                     new XAttribute("type", "application/xml"),
                                     XElement.Parse(item.Content)));
            return element;
        }
    }
}