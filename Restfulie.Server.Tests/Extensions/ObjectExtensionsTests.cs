using System.Collections.Generic;
using NUnit.Framework;
using Restfulie.Server.Tests.Fixtures;
using Restfulie.Server.Extensions;

namespace Restfulie.Server.Tests.Extensions
{
    [TestFixture]
    public class ObjectExtensionsTests
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
        public void ShouldGetPropertyIfItHasOne()
        {
            var resource = new SomeResource { Id = 123 };

            Assert.AreEqual(123, resource.GetProperty("Id"));
            Assert.IsNull(resource.GetProperty("CrazyProperty"));
        }
    }
}
