

using Application.Booking.DTO;
using Application.Booking.Requests;
using Application.Booking.Responses;
using Application.Payment.DTO;
using Application.Payment.Responses;

namespace Application.Booking.Ports
{
    public interface IBookingManager
    {
        Task<BookingResponse> CreateBooking(CreateBookingRequest booking);
        Task<PaymentResponse> PayForABooking(PaymentRequestDTO paymentRequestDTO);
        Task<BookingResponse> GetBooking(int id);
    }
}
