using System.Web.Mvc;
using Moq;
using NUnit.Framework;
using Restfulie.Server.MediaTypes;
using Restfulie.Server.Results;
using Restfulie.Server.Results.Chooser;
using Restfulie.Server.Tests.Fixtures;

namespace Restfulie.Server.Tests.Results.Chooser
{
    [TestFixture]
    public class ResultChooserTests
    {
        private Mock<ResultExecutingContext> context;
        private SomeResult result;

        [SetUp]
        public void SetUp()
        {
            result = new SomeResult();

            context = new Mock<ResultExecutingContext>();
            context.SetupGet(c => c.Controller).Returns(new SomeController{ViewData = new ViewDataDictionary()});
            context.SetupGet(c => c.Result).Returns(result);
        }

        [Test]
        public void ShouldReturnViewResultIfItIsHTML()
        {
            var choosedResult = new ResultChooser().Between(context.Object, new HTML());

            Assert.IsTrue(choosedResult is ViewResult);
        }

        [Test]
        public void ShouldReturnTheSameResultIfItIsNotHTML()
        {
            var choosedResult = (RestfulieResult)new ResultChooser().Between(context.Object, new XmlAndHypermedia());

            Assert.IsTrue(choosedResult is SomeResult);
            Assert.IsTrue(choosedResult.MediaType is XmlAndHypermedia);
        }
    }
}
