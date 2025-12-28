using Conference_Room_Rental.Models;

namespace Conference_Room_Rental.Services
{
    public interface IReservationService
    {
        Task CreateReservationAsync(int conferenceRoomId, string userId, DateTime startTime, DateTime endTime);
        Task<IEnumerable<Models.Reservation>> GetReservationsByUserAsync(string userId);
        Task<Models.Reservation> GetReservationByIdAsync(int reservationId);
        Task<decimal> CalculateTotalCostAsync(int conferenceRoomId, DateTime startTime, DateTime endTime);
        Task<bool> IsRoomAvailableAsync(int conferenceRoomId, DateTime startTime, DateTime endTime);
        Task CancelReservationAsync(int reservationId);
    }
}
