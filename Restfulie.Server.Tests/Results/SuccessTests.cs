using System.IO;
using Moq;
using NUnit.Framework;
using Restfulie.Server.Marshalling;
using Restfulie.Server.Results;
using Restfulie.Server.Tests.Fixtures;

namespace Restfulie.Server.Tests.Results
{
    [TestFixture]
    public class SuccessTests : ResultsTestBase
    {
        private MemoryStream stream;
        private Mock<IResourceMarshaller> builder;
        private SomeResource aSimpleResource;

        [SetUp]
        public void SetUp()
        {
            SetUpRequest();

            stream = new MemoryStream();
            builder = new Mock<IResourceMarshaller>();

            aSimpleResource = new SomeResource { Amount = 123.45, Name = "John Doe" };
        }

        [Test]
        public void ShouldReturnStatusCode200()
        {
            response.SetupSet(c => c.StatusCode = (int)StatusCodes.Success);

            var result = new Success
                             {
                                 Marshaller = new Mock<IResourceMarshaller>().Object
                             };

            result.ExecuteResult(context.Object);

            response.VerifyAll();
        }

        [Test]
        public void ShouldReturnResource()
        {
            response.Setup(p => p.Output).Returns(new StreamWriter(stream));

            builder.Setup(s => s.Build(aSimpleResource)).Returns(
                "<SomeResource><Name>John Doe</name><amount>123.45</amount></SomeResource>");
            var result = new Success(aSimpleResource) {Marshaller = builder.Object};

            result.ExecuteResult(context.Object);

            stream.Seek(0, SeekOrigin.Begin);
            var serializedResource = new StreamReader(stream).ReadToEnd();

            Assert.That(serializedResource.Contains("John Doe"));
            Assert.That(serializedResource.Contains("123.45"));
            builder.VerifyAll();
        }

        [Test]
        public void ShouldReturnListOfResource()
        {
            var resources = new System.Collections.Generic.List<IBehaveAsResource> {aSimpleResource, aSimpleResource};
            response.Setup(p => p.Output).Returns(new StreamWriter(stream));

            builder.Setup(s => s.Build(resources)).Returns("List Of Resources here");
            var result = new Success(resources) { Marshaller = builder.Object };

            result.ExecuteResult(context.Object);

            stream.Seek(0, SeekOrigin.Begin);
            var serializedResource = new StreamReader(stream).ReadToEnd();

            Assert.That(serializedResource.Contains("List Of Resources here"));
            builder.VerifyAll();            
        }
    }
}