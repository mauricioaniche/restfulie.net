namespace Restfulie.Server.Results
{
    public class NotAcceptable : RestfulieResult
    {
        public override int StatusCode
        {
            get { return (int) StatusCodes.NotAcceptable; }
        }
    }
}
