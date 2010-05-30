using System;
using System.Collections.Generic;

// based on http://geekswithblogs.net/lszk/archive/2009/08/23/own-rssatom-feed-in-asp.net-mvc.aspx
namespace Restfulie.Server.Marshalling.Serializers.AtomPlusXml
{
    public class Feed
    {
        //channel informations
        public string Title { set; get; }
        public string Description { set; get; }
        public string Link { set; get; }
        public string Author { set; get; }
        public List<Entry> Items { set; get; }
        public DateTime Updated { set; get; }
    }
}
