using Application.Room.DTO;
using Application.Room.Responses;
using MediatR;

namespace Application.Room.Commands
{
    public class CreateRoomCommand: IRequest<RoomResponse>
    {
        public RoomDTO roomDTO { get; set; }
    }
}
