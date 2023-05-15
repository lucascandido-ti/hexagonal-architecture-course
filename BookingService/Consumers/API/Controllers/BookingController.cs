using Application;
using Application.Booking.Commands;
using Application.Booking.DTO;
using Application.Booking.Ports;
using Application.Booking.Queries;
using Application.Payment.DTO;
using Application.Payment.Responses;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BookingController: ControllerBase
    {
        private readonly ILogger<BookingController> _logger;
        private readonly IBookingManager _bookingManager;
        private readonly IMediator _mediator;
        public BookingController(
            IBookingManager bookingManager,
            ILogger<BookingController> logger,
            IMediator mediator)
        {
            _bookingManager = bookingManager;
            _logger = logger;
            _mediator = mediator;
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
            var command = new CreateBookingCommand
            {
                bookingDTO = booking
            };

            var res = await _mediator.Send(command);

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

        [HttpGet]
        public async Task<ActionResult<BookingDTO>> Get(int id)
        {
            var query = new GetBookingQuery{
                Id = id
            };

            var res = await _mediator.Send(query);

            if (res.Success) return Created("", res.Data);

            _logger.LogError("Could not process the request", res);
            return BadRequest(500);
        }
    }
}
