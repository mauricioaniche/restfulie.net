namespace Restfulie.Server.Results
{
    public class SeeOther : RestfulieResult
    {
        public SeeOther(string location)
        {
            Location = location;
        }

        protected override int StatusCode
        {
            get { return (int) StatusCodes.SeeOther; }
        }
    }
}
