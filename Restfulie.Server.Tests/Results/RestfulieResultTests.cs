using System.IO;
using Moq;
using NUnit.Framework;
using Restfulie.Server.Marshalling;
using Restfulie.Server.Results;
using Restfulie.Server.Tests.Fixtures;

namespace Restfulie.Server.Tests.Results
{
    [TestFixture]
    public class RestfulieResultTests : ResultsTestBase
    {
        private SomeResource aSimpleResource;
        private Mock<IResourceMarshaller> marshaller;

        [SetUp]
        public void SetUp()
        {
            SetUpRequest();

            marshaller = new Mock<IResourceMarshaller>();
            aSimpleResource = new SomeResource { Amount = 123.45, Name = "John Doe" };
        }

        [Test]
        public void ShouldReturnContentType()
        {
            response.SetupSet(c => c.ContentType = "application/xml");
            marshaller.SetupGet(m => m.MediaType).Returns("application/xml");

            var result = new SomeResult(new SomeResource())
            {
                Marshaller = marshaller.Object
            };

            result.ExecuteResult(context.Object);

            response.VerifyAll();
        }


        [Test]
        public void ShouldReturnResource()
        {
            marshaller.Setup(s => s.Build(aSimpleResource)).Returns(
                "<SomeResource><Name>John Doe</name><amount>123.45</amount></SomeResource>");
            var result = new SomeResult(aSimpleResource) { Marshaller = marshaller.Object };

            result.ExecuteResult(context.Object);

            stream.Seek(0, SeekOrigin.Begin);
            var serializedResource = new StreamReader(stream).ReadToEnd();

            Assert.That(serializedResource.Contains("John Doe"));
            Assert.That(serializedResource.Contains("123.45"));
            marshaller.VerifyAll();
        }

        [Test]
        public void ShouldReturnListOfResource()
        {
            var resources = new System.Collections.Generic.List<IBehaveAsResource> { aSimpleResource, aSimpleResource };

            marshaller.Setup(s => s.Build(resources)).Returns("List Of Resources here");
            var result = new SomeResult(resources) { Marshaller = marshaller.Object };

            result.ExecuteResult(context.Object);

            stream.Seek(0, SeekOrigin.Begin);
            var serializedResource = new StreamReader(stream).ReadToEnd();

            Assert.That(serializedResource.Contains("List Of Resources here"));
            marshaller.VerifyAll();
        }

        [Test]
        public void ShouldReturnAMessage()
        {
            response.Setup(p => p.Output).Returns(new StreamWriter(stream));
            var result = new SomeResult("error message");

            result.ExecuteResult(context.Object);

            stream.Seek(0, SeekOrigin.Begin);
            var serializedResource = new StreamReader(stream).ReadToEnd();

            Assert.That(serializedResource.Contains("error message"));
        }
    }
}
