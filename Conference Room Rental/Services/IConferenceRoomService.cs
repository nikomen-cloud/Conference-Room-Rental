using Conference_Room_Rental.Models;

namespace Conference_Room_Rental.Services
{
    public interface IConferenceRoomService
    {
        Task<IEnumerable<ConferenceRoom>> GetAllRoomsAsync();
        Task<ConferenceRoom?> GetRoomByIdAsync(int id);
        Task AddRoomAsync(ConferenceRoom room);
        Task UpdateRoomAsync(ConferenceRoom room);
        Task DeleteRoomAsync(int id);
    }
}