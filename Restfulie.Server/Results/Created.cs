namespace Restfulie.Server.Results
{
    public class Created : RestfulieResult
    {
        public Created() {}
        public Created(IBehaveAsResource resource, string location) : base(resource)
        {
            Location = location;
        }
        public Created(IBehaveAsResource resource) : base(resource) {}

        public Created(string location)
        {
            Location = location;
        }

        public override int StatusCode
        {
            get { return (int)StatusCodes.Created; }
        }
    }
}
