namespace Restfulie.Server.Results
{
    public class PreConditionFailed : RestfulieResult
    {
        public override int StatusCode
        {
            get { return (int) StatusCodes.PreConditionFailed; }
        }
    }
}
