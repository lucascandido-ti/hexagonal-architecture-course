using Domain.Room.Ports;
using Microsoft.EntityFrameworkCore;

namespace Data.Room
{
    public class RoomRepository : IRoomRepository
    {
        private readonly HotelDbContext _hotelDbContext;
        public RoomRepository(HotelDbContext hotelDbContext)
        {
            _hotelDbContext = hotelDbContext;
        }
        public async Task<int> Create(Domain.Room.Entities.Room room)
        {
            _hotelDbContext.Rooms.Add(room);
            await _hotelDbContext.SaveChangesAsync();
            return room.Id;
        }

        public Task<Domain.Room.Entities.Room?> Get(int id)
        {
            return _hotelDbContext.Rooms
                .Where(g => g.Id == id).FirstOrDefaultAsync();
        }

        public Task<Domain.Room.Entities.Room> GetAggregate(int id)
        {
            return _hotelDbContext.Rooms
                .Include(r => r.Bookings)
                .Where(g => g.Id == id).FirstAsync();
        }
    }
}
