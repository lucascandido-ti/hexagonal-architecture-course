using Domain.Room.Ports;
using Microsoft.EntityFrameworkCore;
using Entities = Domain.Entities;

namespace Data.Room
{
    public class RoomRepository : IRoomRepository
    {
        private readonly HotelDbContext _hotelDbContext;
        public RoomRepository(HotelDbContext hotelDbContext)
        {
            _hotelDbContext = hotelDbContext;
        }
        public async Task<int> Create(Entities.Room room)
        {
            _hotelDbContext.Rooms.Add(room);
            await _hotelDbContext.SaveChangesAsync();
            return room.Id;
        }

        public Task<Entities.Room?> Get(int id)
        {
            return _hotelDbContext.Rooms
                .Where(g => g.Id == id).FirstOrDefaultAsync();
        }

        public Task<Entities.Room> GetAggregate(int id)
        {
            return _hotelDbContext.Rooms
                .Include(r => r.Bookings)
                .Where(g => g.Id == id).FirstAsync();
        }
    }
}
