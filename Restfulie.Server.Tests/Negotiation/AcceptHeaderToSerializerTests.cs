using NUnit.Framework;
using Restfulie.Server.Marshalling.Serializers;
using Restfulie.Server.Negotitation;

namespace Restfulie.Server.Tests.Negotiation
{
    [TestFixture]
    public class AcceptHeaderToSerializerTests
    {
        [Test]
        public void ShouldReturnSerializerForAGivenMimeType()
        {
            var serializer = new AcceptHeaderToSerializer().For("application/xml");

            Assert.IsTrue(serializer is XmlAndHypermediaSerializer);
        }
    }
}
