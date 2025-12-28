using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Conference_Room_Rental.Models;
using Conference_Room_Rental.Models.ViewModels;

namespace Conference_Room_Rental.Data
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<ConferenceRoom> ConferenceRooms { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        }
}
