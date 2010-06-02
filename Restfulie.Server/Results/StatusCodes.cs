namespace Restfulie.Server.Results
{
    public enum StatusCodes
    {
        Success = 200,
        Created = 201,
        SeeOther = 303,
        BadRequest = 400,
        NotAcceptable = 406,
        PreConditionFailed = 412,
        UnsupportedMediaType = 415,
        InternalServerError = 500
    }
}