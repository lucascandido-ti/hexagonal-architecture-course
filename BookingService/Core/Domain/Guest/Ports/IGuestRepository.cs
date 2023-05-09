
namespace Domain.Guest.Ports
{
    public interface IGuestRepository
    {
        Task<Entities.Guest> Get(int id);
        Task<int> Create(Entities.Guest guest);
    }
}
