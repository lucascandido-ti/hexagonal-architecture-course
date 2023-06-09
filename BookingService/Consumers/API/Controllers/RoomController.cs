﻿using Application.Room.DTO;
using Application;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using Application.Room.Commands;
using Application.Booking.Queries;
using Application.Room.Queries;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RoomController: ControllerBase
    {
        private readonly ILogger<RoomController> _logger;
        private readonly IMediator _mediator;

        public RoomController(
            ILogger<RoomController> logger,
            IMediator mediator
        )
        {
            _logger = logger;
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResult<RoomDTO>> Post(RoomDTO room)
        {
            var command = new CreateRoomCommand
            {
                roomDTO = room
            };

            var res = await _mediator.Send(command);

            if (res.Success) return Created("", res.Data);

            else if (res.ErrorCode == ErrorCodes.ROOM_MISSING_REQUIRED_INFORMATION)
            {
                return BadRequest(res);
            }
            else if (res.ErrorCode == ErrorCodes.ROOM_COULD_NOT_STORE_DATA)
            {
                return BadRequest(res);
            }

            _logger.LogError("Response with unknown ErrorCode Returned", res);
            return BadRequest(500);
        }

        [HttpGet]
        public async Task<ActionResult<RoomDTO>> Get(int roomId)
        {
            var query = new GetRoomQuery
            {
                Id = roomId
            };

            var res = await _mediator.Send(query);

            if (res.Success) return Created("", res.Data);

            _logger.LogError("Could not process the request", res);
            return BadRequest(500);
        }
    }
}
