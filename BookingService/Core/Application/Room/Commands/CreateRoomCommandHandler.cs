using Application.Room.Ports;
using Application.Room.Requests;
using Application.Room.Responses;
using MediatR;

namespace Application.Room.Commands
{
    public class CreateRoomCommandHandler : IRequestHandler<CreateRoomCommand, RoomResponse>
    {
        private readonly IRoomManager _roomManager;

        public CreateRoomCommandHandler(IRoomManager roomManager)
        {
            _roomManager = roomManager;
        }
        public Task<RoomResponse> Handle(CreateRoomCommand request, CancellationToken cancellationToken)
        {
            var req = new CreateRoomRequest
            {
                Data = request.roomDTO
            };

            return _roomManager.CreateRoom(req);
        }
    }
}
