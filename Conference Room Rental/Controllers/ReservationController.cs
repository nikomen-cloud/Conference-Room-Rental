using Microsoft.AspNetCore.Mvc;
using Conference_Room_Rental.Services;
using Microsoft.AspNetCore.Identity;
using Conference_Room_Rental.Models;
using Microsoft.AspNetCore.Authorization;
using Conference_Room_Rental.Models.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Conference_Room_Rental.Controllers
{
    [Authorize]
    public class ReservationController : Controller
    {
        private readonly IConferenceRoomService _conferenceRoomService;
        private readonly IReservationService _reservationService;
        private readonly UserManager<User> _userManager;

        public ReservationController(IReservationService reservationService, IConferenceRoomService conferenceRoomService, UserManager<User> userManager)
        {
            _reservationService = reservationService;
            _conferenceRoomService = conferenceRoomService;
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return Challenge();
            }
            var reservations = await _reservationService.GetReservationsByUserAsync(user.Id);
            return View(reservations);
        }

        [HttpGet]
        [HttpGet]
        public async Task<IActionResult> Create(int? roomId)
        {
            var model = new CreateReservationViewModel
            {
                StartTime = DateTime.Now,
                EndTime = DateTime.Now.AddHours(1)
            };

            if (roomId.HasValue)
            {
                model.ConferenceRoomId = roomId.Value;
                var room = await _conferenceRoomService.GetRoomByIdAsync(roomId.Value);
                if (room != null)
                {
                    model.SelectedRoomName = room.RoomNumber;
                    model.SelectedRoomDescription = room.Description;
                    model.SelectedRoomImageUrl = room.ImageUrl;
                    model.SelectedRoomPrice = room.PricePerHour;
                }
            }

            await LoadConferenceRoomsToModel(model);
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateReservationViewModel model)
        {
            if (!ModelState.IsValid)
            {
                await LoadConferenceRoomsToModel(model);
                return View(model);
            }

            var user = await _userManager.GetUserAsync(User);
            try
            {
                await _reservationService.CreateReservationAsync(
                    model.ConferenceRoomId.Value,
                    user.Id,
                    model.StartTime,
                    model.EndTime
                );

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);

                await LoadConferenceRoomsToModel(model);
                return View(model);
            }
        }

        private async Task LoadConferenceRoomsToModel(CreateReservationViewModel model)
        {
            var rooms = await _conferenceRoomService.GetAllRoomsAsync();

            model.ConferenceRooms = rooms.Select(cr => new SelectListItem
            {
                Value = cr.Id.ToString(),
                Text = cr.RoomNumber
            });

            if (model.ConferenceRoomId.HasValue)
            {
                var selectedRoom = rooms.FirstOrDefault(r => r.Id == model.ConferenceRoomId.Value);
                if (selectedRoom != null)
                {
                    model.SelectedRoomName = selectedRoom.RoomNumber;
                    model.SelectedRoomDescription = selectedRoom.Description;
                    model.SelectedRoomImageUrl = selectedRoom.ImageUrl;
                    model.SelectedRoomPrice = selectedRoom.PricePerHour;
                }
            }
        }

        public async Task<IActionResult> Details(int id)
        {
            try
            {
                var reservation = await _reservationService.GetReservationByIdAsync(id);
                var user = await _userManager.GetUserAsync(User);

                if (reservation.UserId != user.Id)
                {
                    return Forbid();
                }

                return View(reservation);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var reservation = await _reservationService.GetReservationByIdAsync(id);
                var user = await _userManager.GetUserAsync(User);

                if (reservation.UserId != user.Id)
                {
                    return Forbid();
                }

                await _reservationService.CancelReservationAsync(id);
                return RedirectToAction("Index");
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
            catch (InvalidOperationException ex)
            {
                TempData["Error"] = ex.Message;
                return RedirectToAction("Index");
            }
        }
    }
}