using APBD_T9_s33596.ViewModels;
using APBD_T9_s33596.ViewModels.Response;
using Application.DTOs;
using Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace APBD_T9_s33596.Controllers
{
    [Authorize]
    public class DashboardController : Controller
    {
        private readonly INoteService _noteService;
        public DashboardController(INoteService noteService)
        {
            _noteService = noteService;
        }

        private int CurrentUserId =>
            int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

        public async Task<IActionResult> Index()
        {
            var notes = await _noteService.GetUserNotes(CurrentUserId);

            var viewModel = notes.Select(n => new NoteViewModel
            {
                Id = n.Id,
                Title = n.Title,
                Content = n.Content,
                CreatedAtFormatted = n.CreatedAt.ToString("yyyy-MM-dd")
            }).ToList();

            return View(viewModel);
        }

        [HttpGet]
        public IActionResult AddNote() => View(new AddNoteViewModel());


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddNote(AddNoteViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            AddNoteDto addNote = new()
            {
                UserId = CurrentUserId,
                Title = model.Title,
                Content = model.Content,
                CreatedAt = DateTime.UtcNow
            };

            await _noteService.AddNote(addNote);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteNote(int id)
        {
            DeleteNoteDto deleteNote = new()
            {
                UserId = CurrentUserId,
                NoteId = id
            };

            await _noteService.DeleteNote(deleteNote);
            return RedirectToAction(nameof(Index));
        }
    }
}
