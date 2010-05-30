using NUnit.Framework;
using Restfulie.Server.Marshalling.Serializers.AtomPlusXml;

namespace Restfulie.Server.Tests.Marshalling.Serializers.AtomPlusXml
{
    [TestFixture]
    public class FeedTests
    {
        [Test]
        public void ShouldHaveEntries()
        {
            var feed = new Feed();
            Assert.AreEqual(0, feed.Items.Count);

            feed.Items.Add(new Entry());
            Assert.AreEqual(1, feed.Items.Count);
        }
    }
}
