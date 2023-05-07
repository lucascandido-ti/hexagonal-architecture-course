﻿using Domain.Guest.Ports;
using Microsoft.EntityFrameworkCore;

namespace Data.Guest
{
    public class GuestRepository : IGuestRepository
    {
        private HotelDbContext _hotelDbContext;
        public GuestRepository(HotelDbContext hotelDbContext)
        {
            _hotelDbContext = hotelDbContext;
        }
        public async Task<int> Create(Domain.Guest.Entities.Guest guest)
        {
            _hotelDbContext.Guests.Add(guest);
            await _hotelDbContext.SaveChangesAsync();
            return guest.Id;
        }

        public Task<Domain.Guest.Entities.Guest?> Get(int id)
        {
            return _hotelDbContext.Guests.Where(g => g.Id == id).FirstOrDefaultAsync();
        }
    }
}
