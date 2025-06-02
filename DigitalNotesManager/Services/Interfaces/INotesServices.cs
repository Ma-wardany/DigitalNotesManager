using DigitalNotesManager.Domain.DTOs;
using DigitalNotesManager.Domain.Models;
using DigitalNotesManager.Helpers;

namespace DigitalNotesManager.Services.Interfaces
{
    public interface INotesServices
    {
        public Task<Response<IQueryable<NoteDto>>> GetNotesByCategoryId(int categoryId, int userId);
        public Task<Response<IQueryable<NoteDto>>> GetAllNotes(int userId);
        public Task<Response<NoteDto>> GetNoteById(int noteId, int userId);
        public Task<Response<NoteDto>> AddNote(Note note);
        public Task<Response<string>> DeleteNote(int noteId, int userId);
        public Task<Response<NoteDto>> UpdateNote(Note note);
        public Task<Response<IQueryable<NoteDto>>> FilterNotesByDate(DateTime date, IQueryable<NoteDto> notes);
        public Task<Response<IQueryable<NoteDto>>> SearchNotes(int userId, string searchText);
    }
}
