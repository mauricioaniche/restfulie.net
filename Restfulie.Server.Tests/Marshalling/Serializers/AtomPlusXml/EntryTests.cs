using NUnit.Framework;
using Restfulie.Server.Marshalling.Serializers.AtomPlusXml;

namespace Restfulie.Server.Tests.Marshalling.Serializers.AtomPlusXml
{
    [TestFixture]
    public class EntryTests
    {
        [Test]
        public void ShouldHaveLinks()
        {
            var entry = new Entry();
            Assert.AreEqual(0, entry.Links.Count);

            entry.Links.Add(new Link());
            Assert.AreEqual(1, entry.Links.Count);
        }
    }
}
