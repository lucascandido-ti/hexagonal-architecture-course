using Domain.Utils.Enums;
using Entities = Domain.Room.Entities;
using ValueObjects = Domain.Room.ValueObjects;

namespace Application.Room.DTO
{
    public class RoomDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Level { get; set; }
        public bool InMaintenance { get; set; }
        public decimal Price { get; set; }
        public AcceptedCurrencies Currency { get; set; }

        public static Entities.Room MapToEntity(RoomDTO dto)
        {
            return new Entities.Room
            {
                Id = dto.Id,
                Name = dto.Name,
                Level = dto.Level,
                InMaintenance = dto.InMaintenance,
                Price = new ValueObjects.Price { Currency = dto.Currency, Value = dto.Price },
            };
        }

        public static RoomDTO MapToDto(Entities.Room room)
        {
            return new RoomDTO
            {
                Id = room.Id,
                Name = room.Name,
                Level = room.Level,
                InMaintenance = room.InMaintenance,
            };
        }
    }
}
