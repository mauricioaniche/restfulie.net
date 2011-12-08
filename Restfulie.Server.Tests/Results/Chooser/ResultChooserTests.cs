using System.Web.Mvc;
using Moq;
using NUnit.Framework;
using Restfulie.Server.Http;
using Restfulie.Server.MediaTypes;
using Restfulie.Server.Results;
using Restfulie.Server.Results.Chooser;
using Restfulie.Server.Tests.Fixtures;

namespace Restfulie.Server.Tests.Results.Chooser
{
    [TestFixture]
    public class ResultChooserTests
    {
        private ActionExecutedContext context;
        private SomeResult result;
        private IRequestInfoFinder requestInfo;

        [SetUp]
        public void SetUp()
        {
            result = new SomeResult();

            context = new ActionExecutedContext
                          {
                              Controller = new SomeController {ViewData = new ViewDataDictionary()}, 
                              Result = result
                          };

            requestInfo = new Mock<IRequestInfoFinder>().Object;
        }

        [Test]
        public void ShouldReturnViewResultIfItIsHTMLForOK()
        {
            context.Result = new OK();
            var choosedResult = new ResultChooser().BasedOnMediaType(context, new HTML(), requestInfo);

            Assert.IsTrue(choosedResult is ViewResult);
        }

        [Test]
        public void ShouldReturnViewResultIfItIsHTMLForCreated()
        {
            context.Result = new Created();
            var choosedResult = new ResultChooser().BasedOnMediaType(context, new HTML(), requestInfo);

            Assert.IsTrue(choosedResult is ViewResult);
        }

        [Test]
        public void ShouldNotReturnViewResultIfItIsNotOKOrCreated()
        {
            var choosedResult = new ResultChooser().BasedOnMediaType(context, new HTML(), requestInfo);

            Assert.IsFalse(choosedResult is ViewResult);
        }

        [Test]
        public void ShouldReturnTheSameResultIfItIsNotHTML()
        {
            var choosedResult = (RestfulieResult)new ResultChooser().BasedOnMediaType(context, new XmlAndHypermedia(), requestInfo);

            Assert.IsTrue(choosedResult is SomeResult);
            Assert.IsTrue(choosedResult.MediaType is XmlAndHypermedia);
            Assert.AreEqual(requestInfo, choosedResult.RequestInfo);
        }

        [Test]
        public void ShouldIgnoreNonRestfulieResults()
        {
            context.Result = new RedirectResult("some-url");
            var choosedResult = new ResultChooser().BasedOnMediaType(context, new XmlAndHypermedia(), requestInfo);

            Assert.IsTrue(choosedResult is RedirectResult);
        }
    }
}
