using Application.Booking.Ports;
using Application.Booking.Requests;
using Application.Booking.Responses;
using MediatR;

namespace Application.Booking.Commands
{
    
    public class CreateBookingCommandHandler : IRequestHandler<CreateBookingCommand, BookingResponse>
    {
        private readonly IBookingManager _bookingManager;

        public CreateBookingCommandHandler(IBookingManager bookingManager)
        {
            _bookingManager = bookingManager;
        }

        public Task<BookingResponse> Handle(CreateBookingCommand request, CancellationToken cancellationToken)
        {
            var req = new CreateBookingRequest
            {
                Data = request.bookingDTO
            };

            return _bookingManager.CreateBooking(req);
        }
    }
}
