using Application.Room.DTO;
using Application;
using Application.Room.Ports;
using Microsoft.AspNetCore.Mvc;
using Application.Room.Requests;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RoomController: ControllerBase
    {
        private readonly ILogger<RoomController> _logger;
        private readonly IRoomManager _roomManager;

        public RoomController(
            ILogger<RoomController> logger,
            IRoomManager roomManager)
        {
            _logger = logger;
            _roomManager = roomManager;
        }

        [HttpPost]
        public async Task<ActionResult<RoomDTO>> Post(RoomDTO room)
        {
            var request = new CreateRoomRequest
            {
                Data = room
            };

            var res = await _roomManager.CreateRoom(request);

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
    }
}
