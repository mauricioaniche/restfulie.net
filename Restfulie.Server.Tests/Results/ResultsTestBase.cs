using System.Web;
using System.Web.Mvc;
using Moq;

namespace Restfulie.Server.Tests.Results
{
    public class ResultsTestBase
    {
        protected Mock<HttpResponseBase> response;
        protected Mock<HttpContextBase> http;
        protected Mock<ControllerContext> context;

        protected void SetUpRequest()
        {
            response = new Mock<HttpResponseBase>();
            http = new Mock<HttpContextBase>();
            context = new Mock<ControllerContext>();

            http.Setup(h => h.Response).Returns(response.Object);
            context.Setup(c => c.HttpContext).Returns(http.Object);
        }
    }
}
