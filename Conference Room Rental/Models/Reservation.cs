using Conference_Room_Rental.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Conference_Room_Rental.Models
{
    public class Reservation : IValidatableObject
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [Display(Name = "Sala")]
        public int ConferenceRoomId { get; set; }

        [ForeignKey("ConferenceRoomId")]
        public virtual ConferenceRoom? ConferenceRoom { get; set; }

        [Required]
        public string UserId { get; set; }

        [ForeignKey("UserId")]
        public virtual User? User { get; set; }

        [Required(ErrorMessage = "Data rozpoczęcia jest wymagana")]
        [Display(Name = "Początek rezerwacji")]
        [DataType(DataType.DateTime)]
        public DateTime StartTime { get; set; }

        [Required(ErrorMessage = "Data zakończenia jest wymagana")]
        [Display(Name = "Koniec rezerwacji")]
        [DataType(DataType.DateTime)]
        public DateTime EndTime { get; set; }

        [DataType(DataType.Currency)]
        [Column(TypeName = "decimal(18,2)")]
        [Display(Name = "Całkowity koszt")]
        public decimal TotalCost { get; set; }

        [Required]
        public ReservationStatus Status { get; set; } = ReservationStatus.Pending;

        [Display(Name = "Cel rezerwacji")]
        [StringLength(200, ErrorMessage = "Opis zbyt długi.")]
        public string? Description { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (EndTime <= StartTime)
            {
                yield return new ValidationResult(
                    "Data zakończenia musi być późniejsza niż data rozpoczęcia.",
                    new[] { nameof(EndTime) });
            }

            if (StartTime < DateTime.Now)
            {
                yield return new ValidationResult(
                    "Nie można rezerwować terminu w przeszłości.",
                    new[] { nameof(StartTime) });
            }
        }
    }
}