using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Conference_Room_Rental.Models.ViewModels
{
    public class CreateReservationViewModel
    {
        [Required(ErrorMessage = "Prosze wybrać salę konferencyjną")]
        public int ?ConferenceRoomId { get; set; }
        [Required(ErrorMessage = "Prosze podać date rozpoczęcia najmu")]
        [DataType(DataType.DateTime)]
        public DateTime StartTime { get; set; } = DateTime.Today.AddHours(12);
        [Required(ErrorMessage = "Prosze podać date zakończenia najmu")]
        [DataType(DataType.DateTime)]
        public DateTime EndTime { get; set; } = DateTime.Today.AddHours(14);
        [ValidateNever]
        public IEnumerable<SelectListItem> ConferenceRooms { get; set; }
        public string? SelectedRoomName { get; set; }
        public string? SelectedRoomDescription { get; set; }
        public string? SelectedRoomImageUrl { get; set; }
        public decimal? SelectedRoomPrice { get; set; }
    }
}
