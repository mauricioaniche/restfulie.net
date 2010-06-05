using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Moq;
using NUnit.Framework;
using Restfulie.Server.Tests.Fixtures;
using Restfulie.Server.Unmarshalling;
using Restfulie.Server.Unmarshalling.Deserializers;

namespace Restfulie.Server.Tests.Unmarshalling
{
    [TestFixture]
    public class RestfulieUnmarshallerTests
    {
        private Mock<IResourceDeserializer> deserializer;
        private RestfulieUnmarshaller unmarshaller;

        [SetUp]
        public void SetUp()
        {
            deserializer = new Mock<IResourceDeserializer>();
            unmarshaller = new RestfulieUnmarshaller(deserializer.Object);
        }

        [Test]
        public void ShouldUnmarshallResource()
        {
            deserializer.Setup(d => d.Deserialize(SomeXML(), typeof(SomeResource))).Returns(new SomeResource());
            
            var resource = unmarshaller.Build(SomeXML(), typeof(SomeResource));

            Assert.AreEqual(typeof(SomeResource), resource.GetType());
        }

        [Test]
        public void ShouldNotUnmarshallIfNothingWasPassed()
        {
            deserializer.Setup(d => d.Deserialize(string.Empty, typeof (SomeResource))).Throws(new Exception());
            var resource = unmarshaller.Build(string.Empty, typeof (SomeResource));

            Assert.IsNull(resource);
        }


        [Test]
        public void ShouldThrowUnmarshallingExceptionIfSomethingFails()
        {
            deserializer.Setup(d => d.Deserialize(SomeXML(), typeof(SomeResource))).Throws(new Exception());

            Assert.Throws<UnmarshallingException>(() => unmarshaller.Build(SomeXML(), typeof(SomeResource)));
        }

        [Test]
        public void ShouldUnmarshallListOfResources()
        {
            deserializer.Setup(d => d.Deserialize(SomeXML(), typeof(SomeResource[]))).Returns(new[] {new SomeResource()});

            var resources = (SomeResource[])unmarshaller.Build(SomeXML(), typeof(SomeResource[]));

            Assert.AreEqual(typeof(SomeResource), resources[0].GetType());
        }

        [Test]
        public void ShouldNotUnmarshallListIfNothingWasPassed()
        {
            deserializer.Setup(d => d.Deserialize(string.Empty, typeof(SomeResource))).Throws(new Exception());
            var resource = unmarshaller.Build(string.Empty, typeof(SomeResource));

            Assert.IsNull(resource);
        }


        [Test]
        public void ShouldThrowUnmarshallingExceptionIfSomethingFailsWhenUnmarshallingList()
        {
            deserializer.Setup(d => d.Deserialize(SomeXML(), typeof(SomeResource[]))).Throws(new Exception());

            Assert.Throws<UnmarshallingException>(() => unmarshaller.Build(SomeXML(), typeof(SomeResource[])));
        }

        private string SomeXML()
        {
            return "xml";
        }
    }
}
