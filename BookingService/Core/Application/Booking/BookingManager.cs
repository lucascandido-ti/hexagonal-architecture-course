using Application.Booking.DTO;
using Application.Booking.Ports;
using Application.Booking.Requests;
using Application.Booking.Responses;
using Domain.Booking.Exceptions;
using Domain.Booking.Ports;
using Domain.Guest.Ports;
using Domain.Room.Ports;

namespace Application.Booking
{
    public class BookingManager : IBookingManager
    {

        private readonly IBookingRepository _bookingRepository;
        private readonly IGuestRepository _guestRepository;
        private readonly IRoomRepository _roomRepository;

        public BookingManager(
            IBookingRepository bookingRepository,
            IGuestRepository guestRepository,
            IRoomRepository roomRepository
        )
        {
            _bookingRepository = bookingRepository;
            _guestRepository = guestRepository;
            _roomRepository = roomRepository;
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
            catch (Exception)
            {
                return new BookingResponse
                {
                    Success = false,
                    ErrorCode = ErrorCodes.BOOKING_COULD_NOT_STORE_DATA,
                    Message = "There was an error when saving to DB"
                };
            }


        }

        public Task<BookingResponse> GetBooking(int id)
        {
            throw new NotImplementedException();
        }
    }
}
