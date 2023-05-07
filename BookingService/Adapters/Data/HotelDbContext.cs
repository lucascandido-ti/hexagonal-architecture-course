using Data.Guest;
using Data.Room;
using Domain.Guest.Entities;
using Microsoft.EntityFrameworkCore;

namespace Data
{
    public class HotelDbContext : DbContext
    {
        public HotelDbContext(DbContextOptions<HotelDbContext> options) : base(options) { }

        public virtual DbSet<Domain.Guest.Entities.Guest> Guests { get; set; }

        public virtual DbSet<Domain.Room.Entities.Room> Rooms { get; set; }

        public virtual DbSet<Booking> Bookings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .ApplyConfiguration(new GuestConfiguration())
                .ApplyConfiguration(new RoomConfiguration());
        }
    }
}