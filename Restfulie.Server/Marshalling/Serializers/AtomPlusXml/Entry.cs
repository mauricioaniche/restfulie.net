using System;

// based on http://geekswithblogs.net/lszk/archive/2009/08/23/own-rssatom-feed-in-asp.net-mvc.aspx
namespace Restfulie.Server.Marshalling.Serializers.AtomPlusXml
{
    public class Entry
    {
        //feed informations
        public string Title { set; get; }
        public string Description { set; get; }
        public string PublicDate { set; get; }
        public string Id { set; get; }
        public string Content { get; set; }
    }
}
