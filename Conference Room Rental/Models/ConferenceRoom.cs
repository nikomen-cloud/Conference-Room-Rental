using Conference_Room_Rental.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Conference_Room_Rental.Models
{
    public class ConferenceRoom
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Numer lub nazwa sali jest wymagana.")]
        [StringLength(50, ErrorMessage = "Nazwa sali nie może przekraczać 50 znaków.")]
        [Display(Name = "Numer/Nazwa Sali")]
        public string RoomNumber { get; set; }

        [Required(ErrorMessage = "Lokalizacja jest wymagana.")]
        [StringLength(100)]
        [Display(Name = "Lokalizacja (Piętro/Budynek)")]
        public string Location { get; set; }

        [Display(Name = "Opis sali")]
        [StringLength(500, ErrorMessage = "Opis jest zbyt długi.")]
        public string? Description { get; set; }

        [Required]
        [Range(1, 1000, ErrorMessage = "Pojemność musi wynosić od 1 do 1000 osób.")]
        [Display(Name = "Pojemność (osób)")]
        public int Capacity { get; set; }

        [Required]
        [Range(0, 10000, ErrorMessage = "Cena musi być wartością dodatnią.")]
        [DataType(DataType.Currency)]
        [Column(TypeName = "decimal(18,2)")]
        [Display(Name = "Cena za godzinę (PLN)")]
        public decimal PricePerHour { get; set; }

        [Display(Name = "Zdjęcie (URL)")]
        [Url(ErrorMessage = "To nie jest poprawny adres URL.")]
        public string? ImageUrl { get; set; }

        [Display(Name = "Rzutnik / Projektor")]
        public bool HasProjector { get; set; }

        [Display(Name = "Klimatyzacja")]
        public bool HasAirConditioning { get; set; }

        [Display(Name = "Dostęp do Wi-Fi")]
        public bool HasWiFi { get; set; }

        [Display(Name = "Tablica suchościeralna")]
        public bool HasWhiteboard { get; set; }

        [Display(Name = "Aktywna")]
        public bool IsActive { get; set; } = true;

        public virtual ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();
    }
}