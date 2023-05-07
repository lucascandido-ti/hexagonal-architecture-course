
namespace Application
{
    public enum ErrorCodes
    {
        // Guests
        NOT_FOUND = 1,
        COULD_NOT_STORE_DATA,
        INVALID_PERSON_ID,
        MISSING_REQUIRED_INFORMATION,
        INVALID_EMAIL,
        GUEST_NOT_FOUND,

        // Rooms
        ROOM_NOT_FOUND = 100,
        ROOM_COULD_NOT_STORE_DATA,
        ROOM_INVALID_PERSON_ID,
        ROOM_MISSING_REQUIRED_INFORMATION,
        ROOM_INVALID_EMAIL,
    }
    public abstract class Response
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public ErrorCodes ErrorCode { get; set; }
    }
}
