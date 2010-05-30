using System;
using System.Collections.Generic;

// based on http://geekswithblogs.net/lszk/archive/2009/08/23/own-rssatom-feed-in-asp.net-mvc.aspx
namespace Restfulie.Server.Marshalling.Serializers.AtomPlusXml
{
    public class Entry
    {
        public Entry()
        {
            Links = new List<Link>();
        }
        //feed informations
        public string Title { set; get; }
        public string Description { set; get; }
        public DateTime PublicDate { set; get; }
        public string Id { set; get; }
        public List<Link> Links { get; set; }
        public string Content { get; set; }
    }
}
