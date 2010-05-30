namespace Restfulie.Server.Results
{
    public class NotAcceptable : RestfulieResult
    {
        protected override int StatusCode
        {
            get { return (int) StatusCodes.NotAcceptable; }
        }
    }
}
