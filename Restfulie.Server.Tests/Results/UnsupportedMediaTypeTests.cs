using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Moq;
using NUnit.Framework;
using Restfulie.Server.Marshalling;
using Restfulie.Server.Results;

namespace Restfulie.Server.Tests.Results
{
    [TestFixture]
    public class UnsupportedMediaTypeTests : ResultsTestBase
    {
        [SetUp]
        public void SetUp()
        {
            SetUpRequest();
        }

        [Test]
        public void ShouldReturnStatusCode415()
        {
            response.SetupSet(c => c.StatusCode = (int)StatusCodes.UnsupportedMediaType);

            var result = new UnsupportedMediaType
            {
                Marshaller = new Mock<IResourceMarshaller>().Object
            };

            result.ExecuteResult(context.Object);

            response.VerifySet(c => c.StatusCode = (int)StatusCodes.UnsupportedMediaType);
        }
    }

}
