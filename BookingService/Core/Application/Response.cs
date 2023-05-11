
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

        // Booking
        BOOKING_NOT_FOUND = 200,
        BOOKING_COULD_NOT_STORE_DATA,
        BOOKING_INVALID_PERSON_ID,
        BOOKING_MISSING_REQUIRED_INFORMATION,
        BOOKING_INVALID_EMAIL,
        BOOKING_ROOM_CANNOT_BE_BOOKED,

        // Payment
        PAYMENT_INVALID_PAYMENT_INTENTION = 500,
        PAYMENT_PROVIDER_NOT_IMPLEMENTED = 501
    }
    public abstract class Response
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public ErrorCodes ErrorCode { get; set; }
    }
}
