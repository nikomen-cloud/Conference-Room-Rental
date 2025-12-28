using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Conference_Room_Rental.Models
{
    public class User : IdentityUser
    {
        [Required(ErrorMessage ="Imie jest wymagane")]
        [Display(Name ="Imię")]
        public string? FirstName { get; set; }
        [Required(ErrorMessage ="Nazwisko jest wymagane")]
        [Display(Name ="Nazwisko")]
        public string? LastName { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public ICollection<Reservation>? Reservations { get; set; }
    }
}
