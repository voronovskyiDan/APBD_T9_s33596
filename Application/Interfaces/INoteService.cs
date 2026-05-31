using Application.DTOs;

namespace Application.Interfaces
{
    public interface INoteService
    {
        public Task<List<NoteDto>> GetUserNotes(int userId);
        public Task AddNote(AddNoteDto addNote);
        public Task DeleteNote(DeleteNoteDto deleteNote);
    }
}
