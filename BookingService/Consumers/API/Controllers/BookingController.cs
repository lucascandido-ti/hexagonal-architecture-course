using Application;
using Application.Booking.DTO;
using Application.Booking.Ports;
using Application.Booking.Requests;
using Application.Payment.DTO;
using Application.Payment.Responses;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BookingController: ControllerBase
    {
        private readonly ILogger<BookingController> _logger;
        private readonly IBookingManager _bookingManager;
        public BookingController(
            IBookingManager bookingManager,
            ILogger<BookingController> logger)
        {
            _bookingManager = bookingManager;
            _logger = logger;
        }

        [HttpPost]
        [Route("{bookingId}/pay")]
        public async Task<ActionResult<PaymentResponse>> Pay(PaymentRequestDTO paymentRequestDto, int bookingId)
        {
            paymentRequestDto.BookingId = bookingId;
            var res = await _bookingManager.PayForABooking(paymentRequestDto);

            if (res.Success) return Ok(res.Data);

            return BadRequest(res);
        }


        [HttpPost]
        public async Task<ActionResult<BookingDTO>> Post(BookingDTO booking)
        {

            var request = new CreateBookingRequest
            {
                Data = booking
            };

            var res = await _bookingManager.CreateBooking(request);

            if (res.Success) return Created("", res.Data);

            else if (res.ErrorCode == ErrorCodes.BOOKING_MISSING_REQUIRED_INFORMATION)
            {
                return BadRequest(res);
            }
            else if (res.ErrorCode == ErrorCodes.BOOKING_COULD_NOT_STORE_DATA)
            {
                return BadRequest(res);
            }
            _logger.LogError("Response with unknown ErrorCode Returned", res);
            return BadRequest(500);
        }
    }
}
