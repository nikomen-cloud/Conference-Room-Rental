using Conference_Room_Rental.Data;
using Microsoft.EntityFrameworkCore;
using Conference_Room_Rental.Models;

namespace Conference_Room_Rental.Services
{
    public class ReservationService : IReservationService
    {
        private readonly ApplicationDbContext _context;

        public ReservationService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task CreateReservationAsync(int conferenceRoomId, string userId, DateTime startTime, DateTime endTime)
        {
            if(startTime >= endTime)
            {
                throw new ArgumentException("End time must be after start time.");
            }

            var isAvailable = await IsRoomAvailableAsync(conferenceRoomId, startTime, endTime);
            if (!isAvailable)
            {
                throw new InvalidOperationException("The conference room is not available for the selected time period.");
            }

            var totalCost = await CalculateTotalCostAsync(conferenceRoomId, startTime, endTime);

            var reservation = new Models.Reservation
            {
                ConferenceRoomId = conferenceRoomId,
                UserId = userId,
                StartTime = startTime,
                EndTime = endTime,
                TotalCost = totalCost,
                Status = Models.ReservationStatus.Pending
            };

            _context.Reservations.Add(reservation);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Models.Reservation>> GetReservationsByUserAsync(string userId)
        {
            return await _context.Reservations
                .Include(r => r.ConferenceRoom)
                .Where(r => r.UserId == userId)
                .ToListAsync();
        }
        public async Task<Models.Reservation> GetReservationByIdAsync(int reservationId)
        {
            var reservation = await _context.Reservations
                .Include(r => r.ConferenceRoom)
                .FirstOrDefaultAsync(r => r.Id == reservationId);
            if (reservation == null)
            {
                throw new KeyNotFoundException("Reservation not found.");
            }
            return reservation;
        }

        public async Task<decimal> CalculateTotalCostAsync(int conferenceRoomId, DateTime startTime, DateTime endTime)
        {
            var conferenceRoom = await _context.ConferenceRooms.FindAsync(conferenceRoomId);

            if (conferenceRoom == null)
            {
                throw new ArgumentException("Invalid conference room ID.");
            }

            var duration = endTime - startTime;
            var totalHours = (decimal)duration.TotalHours;
            var totalCost = totalHours * conferenceRoom.PricePerHour;
            return totalCost;
        }

        public async Task<bool> IsRoomAvailableAsync(int conferenceRoomId, DateTime startTime, DateTime endTime)
        {
            var overlappingReservations = await _context.Reservations
                .AnyAsync(r => r.ConferenceRoomId == conferenceRoomId &&
                               r.Status != Models.ReservationStatus.Cancelled &&
                               r.Status != Models.ReservationStatus.Rejected &&
                               r.StartTime < endTime &&
                               r.EndTime > startTime);
            return !overlappingReservations;
        }

        public async Task CancelReservationAsync(int reservationId)
        {
            var reservation = await _context.Reservations.FindAsync(reservationId);
            if (reservation == null)
            {
                throw new KeyNotFoundException("Invalid reservation ID.");
            }
            reservation.Status = Models.ReservationStatus.Cancelled;
            await _context.SaveChangesAsync();
        }       
    }
}
