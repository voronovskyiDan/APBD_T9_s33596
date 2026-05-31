using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;
using Domain.Repositories;

namespace Application.Services
{
    public class NoteService : INoteService
    {
        private readonly INoteRepository _noteRepository;

        public NoteService(INoteRepository noteRepository)
        {
            _noteRepository = noteRepository;
        }

        public Task AddNote(AddNoteDto addNote)
        {
            var note = new Note
            {
                UserId = addNote.UserId,
                Title = addNote.Title,
                Content = addNote.Content,
                CreatedAt = DateTime.UtcNow
            };
            return _noteRepository.AddAsync(note);
        }

        public async Task DeleteNote(DeleteNoteDto deleteNote)
        {
            var note = await _noteRepository.GetByIdAsync(deleteNote.NoteId, deleteNote.UserId);
            if (note == null)
                return;
            await _noteRepository.DeleteAsync(note);
        }

        public async Task<List<NoteDto>> GetUserNotes(int userId)
        {
            var notes = await _noteRepository.GetByUserIdAsync(userId);

            return notes.Select(n => new NoteDto
            {
                Id = n.Id,
                Title = n.Title,
                Content = n.Content,
                CreatedAt = n.CreatedAt
            }).ToList();
        }
    }
}
