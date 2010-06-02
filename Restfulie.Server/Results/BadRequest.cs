namespace Restfulie.Server.Results
{
    public class BadRequest : RestfulieResult
    {
        public override int StatusCode
        {
            get { return (int) StatusCodes.BadRequest; }
        }
    }
}