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
        [Test]
        public void ShouldUnmarshallResource()
        {
            var deserializer = new Mock<IResourceDeserializer>();
            var unmarshaller = new DefaultResourceUnmarshaller(deserializer.Object);

            deserializer.Setup(d => d.Deserialize<SomeResource>(SomeXML())).Returns(new SomeResource());
            
            var resource = unmarshaller.ToResource<SomeResource>(SomeXML());

            Assert.AreEqual(typeof(SomeResource), resource.GetType());
        }

        private string SomeXML()
        {
            return "xml";
        }
    }
}
