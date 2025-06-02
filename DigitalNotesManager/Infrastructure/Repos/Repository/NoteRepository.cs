using DigitalNotesManager.Domain.Models;
using DigitalNotesManager.Infrastructure.Data;
using DigitalNotesManager.Infrastructure.Repos.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DigitalNotesManager.Infrastructure.Repos.Repository
{

    public class NoteRepository : INoteRepository
    {
        private readonly DataContext _context;
        public NoteRepository()
        {
            _context = new DataContext();
        }

        public async Task AddNoteAsync(Note note)
        {
            await _context.Notes.AddAsync(note);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteNoteAsync(Note note)
        {
            _context.Notes.Remove(note);
            await _context.SaveChangesAsync();
        }

        public IQueryable<Note> GetNoteByCategory(int CategoryId, int userId)
        {
            return _context.Notes.Where(n => n.CategoryId == CategoryId && n.UserId == userId);
        }
            
        public async Task<Note> GetNoteById(int noteId)
        {
            return await _context.Notes.FindAsync(noteId);
        }

        public Task<Note> GetNoteByTitle(string Title)
        {
            return _context.Notes.FirstOrDefaultAsync(n => n.Title.Equals(Title, StringComparison.OrdinalIgnoreCase));
        }

        public IQueryable<Note> GetNotesByUserId(int UserId)
        {
            return _context.Notes.Where(n => n.UserId == UserId);
        }

        public async Task UpdateNoteAsync(Note note)
        {
            _context.Notes.Update(note);
            await _context.SaveChangesAsync();
        }
    }
}
