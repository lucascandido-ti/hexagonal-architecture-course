
using GuestEntities = Domain.Guest.Entities;

namespace Domain.Guest.Ports
{
    public interface IGuestRepository
    {
        Task<GuestEntities.Guest> Get(int id);
        Task<int> Create(GuestEntities.Guest guest);
    }
}
