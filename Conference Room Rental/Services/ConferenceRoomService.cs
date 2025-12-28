using Conference_Room_Rental.Data;
using Conference_Room_Rental.Models;
using Microsoft.EntityFrameworkCore;

namespace Conference_Room_Rental.Services
{
    public class ConferenceRoomService : IConferenceRoomService
    {
        private readonly ApplicationDbContext _context;

        public ConferenceRoomService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ConferenceRoom>> GetAllRoomsAsync()
        {
            return await _context.ConferenceRooms.ToListAsync();
        }

        public async Task<ConferenceRoom?> GetRoomByIdAsync(int id)
        {
            return await _context.ConferenceRooms.FirstOrDefaultAsync(r => r.Id == id);
        }
        public async Task AddRoomAsync(ConferenceRoom room)
        {
            _context.ConferenceRooms.Add(room);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateRoomAsync(ConferenceRoom room)
        {
            _context.ConferenceRooms.Update(room);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteRoomAsync(int id)
        {
            var room = await _context.ConferenceRooms.FindAsync(id);
            if (room != null)
            {
                _context.ConferenceRooms.Remove(room);
                await _context.SaveChangesAsync();
            }
        }
    }
}