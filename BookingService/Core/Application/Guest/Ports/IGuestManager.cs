using Application.Guest.Requests;
using Application.Guest.Responses;

namespace Application.Guest.Ports
{
    public interface IGuestManager
    {
        Task<GuestResponse> CreateGuest(CreateGuestRequest request);
        Task<GuestResponse> GetGuest(int guestId);
    }
}
