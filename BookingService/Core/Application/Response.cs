
namespace Application
{
    public enum ErrorCodes
    {
        NOT_FOUND = 1,
        COULD_NOT_STORE_DATA,
        INVALID_PERSON_ID,
        MISSING_REQUIRED_INFORMATION,
        INVALID_EMAIL
    }
    public abstract class Response
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public ErrorCodes ErrorCode { get; set; }
    }
}
