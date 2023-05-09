

using Application.Booking.DTO;
using Application.Booking.Requests;
using Application.Booking.Responses;

namespace Application.Booking.Ports
{
    public interface IBookingManager
    {
        Task<BookingResponse> CreateBooking(CreateBookingRequest booking);

        Task<BookingResponse> GetBooking(int id);
    }
}
