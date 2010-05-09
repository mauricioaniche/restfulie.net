using System.Web.Mvc;

namespace Restfulie.Server.Results
{
    public class BadRequest : RestfulieResult
    {
        private readonly string message;

        public BadRequest()
        {
        }

        public BadRequest(string message) : this()
        {
            this.message = message;
        }

        public override void ExecuteResult(ControllerContext context)
        {
            SetStatusCode(context, StatusCodes.BadRequest);

            if(MessageWasPassed())
            {
                Write(context, message);
            }
        }

        private bool MessageWasPassed()
        {
            return !string.IsNullOrEmpty(message);
        }
    }
}