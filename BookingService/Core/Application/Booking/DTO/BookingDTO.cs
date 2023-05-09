using Entities = Domain.Entities;
using Domain.Guest.Enums;

namespace Application.Booking.DTO
{
    public class BookingDTO
    {

        public BookingDTO()
        {
            this.PlacedAt = DateTime.UtcNow;
        }
        public int Id { get; set; }
        public DateTime PlacedAt { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public int RoomId { get; set; }
        public int GuestId { get; set; }
        public Status Status { get; set; }

        public static Entities.Booking MapToEntity(BookingDTO dto)
        {
            return new Entities.Booking
            {
                Id = dto.Id,
                Start = dto.Start,
                Guest = new Entities.Guest { Id = dto.GuestId },
                Room = new Entities.Room { Id = dto.RoomId },
                End = dto.End
            };
        }
    }
}
