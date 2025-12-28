using Conference_Room_Rental.Models;
using Conference_Room_Rental.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Conference_Room_Rental.Controllers
{
    public class ConferenceRoomController : Controller
    {
        private readonly IConferenceRoomService _conferenceRoomService;

        public ConferenceRoomController(IConferenceRoomService conferenceRoomService)
        {
            _conferenceRoomService = conferenceRoomService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var rooms = await _conferenceRoomService.GetAllRoomsAsync();
            return View(rooms);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var room = await _conferenceRoomService.GetRoomByIdAsync(id);
            // 4. Pattern matching "is null"
            if (room is null) return NotFound();

            return View(room);
        }


        [Authorize(Roles = "Admin")]
        public IActionResult Create() => View();

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Create(ConferenceRoom room)
        {
            if (!ModelState.IsValid) return View(room); 

            await _conferenceRoomService.AddRoomAsync(room);
            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id)
        {
            var room = await _conferenceRoomService.GetRoomByIdAsync(id);
            if (room is null) return NotFound();

            return View(room);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Edit(int id, ConferenceRoom room)
        {
            if (id != room.Id) return NotFound();

            if (!ModelState.IsValid) return View(room);

            await _conferenceRoomService.UpdateRoomAsync(room);
            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var room = await _conferenceRoomService.GetRoomByIdAsync(id);
            if (room is null) return NotFound();

            return View(room);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _conferenceRoomService.DeleteRoomAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}