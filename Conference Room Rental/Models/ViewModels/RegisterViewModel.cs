using System.ComponentModel.DataAnnotations;

namespace Conference_Room_Rental.Models.ViewModels
{
    public class RegisterViewModel
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Imię jest wymagane")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Nazwisko jest wymagane")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Email jest wymagany")]
        [EmailAddress(ErrorMessage = "Niepoprawny format email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Hasło jest wymagane")]
        [DataType(DataType.Password)]
        [MinLength(8, ErrorMessage = "Hasło musi mieć minimum 8 znaków")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Potwierdź hasło")]
        [Compare("Password", ErrorMessage = "Hasła nie są identyczne")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Numer telefonu jest wymagany")]
        [Display(Name = "Numer telefonu")]
        [StringLength(15, ErrorMessage = "Numer telefonu nie może przekraczać 15 znaków")]
        public string PhoneNumber { get; set; }

    }
}