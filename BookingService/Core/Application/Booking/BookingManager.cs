using Application.Booking.DTO;
using Application.Booking.Ports;
using Application.Booking.Requests;
using Application.Booking.Responses;
using Application.Payment.DTO;
using Application.Payment.Ports;
using Application.Payment.Responses;
using Domain.Booking.Exceptions;
using Domain.Booking.Ports;
using Domain.Guest.Ports;
using Domain.Room.Exceptions;
using Domain.Room.Ports;

namespace Application.Booking
{
    public class BookingManager : IBookingManager
    {

        private readonly IBookingRepository _bookingRepository;
        private readonly IGuestRepository _guestRepository;
        private readonly IRoomRepository _roomRepository;
        private readonly IPaymentProcessorFactory _paymentProcessorFactory;

        public BookingManager(
            IBookingRepository bookingRepository,
            IGuestRepository guestRepository,
            IRoomRepository roomRepository,
            IPaymentProcessorFactory paymentProcessorFactory
        )
        {
            _bookingRepository = bookingRepository;
            _guestRepository = guestRepository;
            _roomRepository = roomRepository;
            _paymentProcessorFactory = paymentProcessorFactory;
        }
        public async Task<BookingResponse> CreateBooking(CreateBookingRequest request)
        {
            try
            {
                var booking = BookingDTO.MapToEntity(request.Data);


                booking.Guest = await _guestRepository.Get(request.Data.GuestId);
                booking.Room = await _roomRepository.Get(request.Data.RoomId);

                await booking.Save(_bookingRepository);

                request.Data.Id = booking.Id;

                return new BookingResponse
                {
                    Success = true,
                    Data = request.Data,
                };
            }
            catch (PlaceAtRequiredInformationException)
            {
                return new BookingResponse
                {
                    Success = false,
                    ErrorCode = ErrorCodes.BOOKING_MISSING_REQUIRED_INFORMATION,
                    Message = "PlacedAt is a required information"
                };
            }
            catch (StartDateRequiredException)
            {
                return new BookingResponse
                {
                    Success = false,
                    ErrorCode = ErrorCodes.BOOKING_MISSING_REQUIRED_INFORMATION,
                    Message = "Start is a required information"
                };
            }
            catch (RoomRequiredException)
            {
                return new BookingResponse
                {
                    Success = false,
                    ErrorCode = ErrorCodes.BOOKING_MISSING_REQUIRED_INFORMATION,
                    Message = "Room is a required information"
                };
            }catch (RoomCannotBeBookedException)
            {
                return new BookingResponse
                {
                    Success = false,
                    ErrorCode = ErrorCodes.BOOKING_ROOM_CANNOT_BE_BOOKED,
                    Message = "The selected Room is not avaliable"
                };
            }
            catch (GuestRequiredException)
            {
                return new BookingResponse
                {
                    Success = false,
                    ErrorCode = ErrorCodes.BOOKING_MISSING_REQUIRED_INFORMATION,
                    Message = "Guest is a required information"
                };
            }
            catch (Exception ex)
            {
                return new BookingResponse
                {
                    Success = false,
                    ErrorCode = ErrorCodes.BOOKING_COULD_NOT_STORE_DATA,
                    Message = ex.Message
                };
            }


        }

        public async Task<PaymentResponse> PayForABooking(PaymentRequestDTO paymentRequestDTO)
        {
            var paymentProcessor = _paymentProcessorFactory.GetPaymentProcessor(paymentRequestDTO.SelectedPaymentProvider);

            var response = await paymentProcessor.CapturePayment(paymentRequestDTO.PaymentIntention);

            if(response.Success)
            {
                return new PaymentResponse
                {
                    Success = true,
                    Data = response.Data,
                    Message = "Payment successfully processed"
                };
            }
            return response;
        }

        public Task<BookingResponse> GetBooking(int id)
        {
            throw new NotImplementedException();
        }

    }
}
