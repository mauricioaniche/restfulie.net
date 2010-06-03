using System.Web.Mvc;

namespace Restfulie.Server.Marshalling
{
    public class ResponseInfo
    {
        public string Location { get; set; }
        public int StatusCode { get; set; }
        public ViewDataDictionary ViewData { get; set; }
        public TempDataDictionary TempData { get; set; }
    }
}
