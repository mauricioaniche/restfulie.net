using System.Collections.Generic;
using NUnit.Framework;
using Restfulie.Server.Tests.Fixtures;
using Restfulie.Server.Extensions;

namespace Restfulie.Server.Tests.Extensions
{
    [TestFixture]
    public class ObjectExtensionsTest
    {
        [Test]
        public void ShouldConvertToAnArrayOfResources()
        {
            var resource = new SomeResource();
            var list = new List<SomeResource> {resource};

            var array = list.AsResourceArray();

            Assert.AreEqual(1, array.Length);
            Assert.AreEqual(resource, array[0]);
        }

        [Test]
        public void ShouldGetIdPropertyIfItHasOne()
        {
            var resourceWithId = new SomeResourceWithId { Id = 123 };
            var resourceWithoutId = new SomeResource();

            Assert.AreEqual(123, resourceWithId.GetIdProperty().Value);
            Assert.IsNull(resourceWithoutId.GetIdProperty());
        }
    }
}
