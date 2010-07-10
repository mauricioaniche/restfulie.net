using System.Web.Mvc;
using NUnit.Framework;
using Restfulie.Server.Results;
using Restfulie.Server.Extensions;

namespace Restfulie.Server.Tests.Extensions
{
    [TestFixture]
    public class ActionResultExtensionsTests
    {
        [Test]
        public void ShouldKnowIfItIsARestfulieResult()
        {
            Assert.IsTrue(new OK().IsRestfulieResult());
            Assert.IsFalse(new RedirectResult("url").IsRestfulieResult());
        }
    }
}
