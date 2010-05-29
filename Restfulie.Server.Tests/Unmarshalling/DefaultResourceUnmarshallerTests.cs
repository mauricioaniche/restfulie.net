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
    public class DefaultResourceUnmarshallerTests
    {
        private Mock<IResourceDeserializer> deserializer;
        private DefaultResourceUnmarshaller unmarshaller;

        [SetUp]
        public void SetUp()
        {
            deserializer = new Mock<IResourceDeserializer>();
            unmarshaller = new DefaultResourceUnmarshaller(deserializer.Object);
        }

        [Test]
        public void ShouldUnmarshallResource()
        {
            deserializer.Setup(d => d.Deserialize(SomeXML(), typeof(SomeResource))).Returns(new SomeResource());
            
            var resource = unmarshaller.ToResource(SomeXML(), typeof(SomeResource));

            Assert.AreEqual(typeof(SomeResource), resource.GetType());
        }

        [Test]
        public void ShouldNotUnmarshallIfNothingWasPassed()
        {
            deserializer.Setup(d => d.Deserialize(string.Empty, typeof (SomeResource))).Throws(new Exception());
            var resource = unmarshaller.ToResource(string.Empty, typeof (SomeResource));

            Assert.IsNull(resource);
        }

        private string SomeXML()
        {
            return "xml";
        }
    }
}
