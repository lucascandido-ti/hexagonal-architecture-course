using Domain.Booking.Ports;
using Entities = Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Data.Booking
{
    public class BookingRepository : IBookingRepository
    {
        private readonly HotelDbContext _dbContext;
        public BookingRepository(HotelDbContext hotelDbContext)
        {
            _dbContext = hotelDbContext;
        }
        public async Task<Entities.Booking> CreateBooking(Entities.Booking booking)
        {
            _dbContext.Bookings.Add(booking);
            await _dbContext.SaveChangesAsync();
            return booking;
        }

        public Task<Entities.Booking> Get(int id)
        {
            return _dbContext.Bookings.Include(b => b.Guest).Include(b => b.Room).Where(x => x.Id == id).FirstAsync();
        }

    }
}
