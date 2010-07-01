using System;
using NUnit.Framework;
using Restfulie.Server.Extensions;

namespace Restfulie.Server.Tests.Extensions
{
    [TestFixture]
    public class DateTimeExtensionsTests
    {
        [Test]
        public void ShouldReturnADateInRFC3339Format()
        {
            var date = new DateTime(2009, 10, 12, 10, 11, 12);

            Assert.AreEqual("2009-10-12T10:11:12.000", date.ToRFC3339());
        }
    }
}
