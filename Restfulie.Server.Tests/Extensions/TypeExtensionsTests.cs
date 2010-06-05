using System.Collections.Generic;
using NUnit.Framework;
using Restfulie.Server.Extensions;
using Restfulie.Server.Tests.Fixtures;

namespace Restfulie.Server.Tests.Extensions
{
    [TestFixture]
    public class TypeExtensionsTests
    {
        [Test]
        public void ShouldKnowIfATypeIsAResource()
        {
            Assert.IsTrue(new SomeResource().GetType().IsAResource());
            Assert.IsFalse(123.GetType().IsAResource());
        }

        [Test]
        public void SholdKnowIfATypeIsAListOfResources()
        {
            var list = new List<IBehaveAsResource>();
            var array = new[] {new SomeResource()};

            var notAResourceList = new List<int>();
            var notAResourceArray = new[] {1, 2};

            Assert.IsTrue(list.GetType().IsAListOfResources());
            Assert.IsTrue(array.GetType().IsAListOfResources());

            Assert.IsFalse(notAResourceList.GetType().IsAListOfResources());
            Assert.IsFalse(notAResourceArray.GetType().IsAListOfResources());
        }
    }
}
