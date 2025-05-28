using DigitalNotesManager.Data;
using DigitalNotesManager.Interfaces;
using DigitalNotesManager.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalNotesManager.Repository
{

    public class NoteService : INoteService
    {
        private readonly DataContext _context;
        public NoteService(DataContext context)
        {
            _context = context;
        }
        public async Task AddNoteAsync(Note note)
        {
           note.CreationDate = DateTime.Now;
            _context.Notes.Add(note);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteNoteAsync(int id)
        {
            var note = await _context.Notes.FindAsync(id);
            if (note != null)
            {
                _context.Notes.Remove(note);
                await _context.SaveChangesAsync();
            }
            
        }

        public async Task<List<Note>> GetAllNotesAsync(int id)
        {
            return await _context.Notes
                .Where(n => n.userId == id).ToListAsync();
        }

        public async Task<Note> GetNoteById(int id)
        {
            return await _context.Notes.FirstOrDefaultAsync(n => n.noteId == id);

        }

        public async Task UpdateNoteAsync(Note note)
        {
            _context.Notes.Update(note);
            await _context.SaveChangesAsync();
        }
    }
}
