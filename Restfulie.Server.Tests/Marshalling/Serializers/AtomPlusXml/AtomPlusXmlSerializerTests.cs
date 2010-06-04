using System.Collections.Generic;
using NUnit.Framework;
using Restfulie.Server.Marshalling.Serializers;
using Restfulie.Server.Marshalling.Serializers.AtomPlusXml;
using Restfulie.Server.Tests.Fixtures;

namespace Restfulie.Server.Tests.Marshalling.Serializers.AtomPlusXml
{
    [TestFixture]
    public class AtomPlusXmlSerializerTests
    {
        private IResourceSerializer serializer;

        [SetUp]
        public void SetUp()
        {
            serializer = new AtomPlusXmlSerializer();
        }

        [Test]
        public void ShouldSerializeAResource()
        {
            var resource = new SomeResource { Name = "John Doe", Amount = 123.45 };
            var atom = serializer.Serialize(resource, SomeRelations());

            Assert.That(atom.Contains("<entry"));
            Assert.That(atom.Contains("John Doe"));
            Assert.That(atom.Contains("<link rel=\"pay\" href=\"http://some/url\" />"));
            Assert.That(atom.Contains("</entry>"));
        }

        [Test]
        public void ShouldSerializeAListOfResources()
        {
            var resourcesXrelations = new Dictionary<IBehaveAsResource, IList<Relation>>
                                          {
                                              {new SomeResource {Amount = 123.45, Name = "John Doe"}, SomeRelations()},
                                              {new SomeResource {Amount = 67.89, Name = "Sally Doe"}, SomeRelations()}
                                          };

            var atom = serializer.Serialize(resourcesXrelations);

            Assert.That(atom.Contains("<feed xmlns=\"http://www.w3.org/2005/Atom\">"));
            Assert.That(atom.Contains("John Doe"));
            Assert.That(atom.Contains("Sally Doe"));
            Assert.That(atom.Contains("</feed>"));
        }

        private IList<Relation> SomeRelations()
        {
            return new List<Relation> { new Relation("pay", "controller", "action", new Dictionary<string, object>(),  "http://some/url") };
        }
    }
}
