using System.Collections.Generic;

namespace Restfulie.Server.Marshalling
{
    public class MarshallingInfo
    {
        public IBehaveAsResource Resource { get; set; }
        public IEnumerable<IBehaveAsResource> Resources { get; set; }
        public string Message { get; set; }
        public string Location { get; set; }
        public int StatusCode { get; set; }

        public bool HasResource()
        {
            return Resource != null;
        }

        public bool HasResources()
        {
            return Resources != null;
        }

        public bool HasMessage()
        {
            return !string.IsNullOrEmpty(Message);
        }
    }
}
