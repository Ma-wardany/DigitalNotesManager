using DigitalNotesManager.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalNotesManager.Infrastructure.Repos.Interfaces
{
    public interface INoteRepository
    {
        public Task AddNoteAsync(Note note);

        public Task DeleteNoteAsync(Note note);

        public IQueryable<Note> GetNoteByCategory(int CategoryId, int userId);

        public Task<Note> GetNoteById(int noteId);

        public Task<Note> GetNoteByTitle(string Title);
        public IQueryable<Note> GetNotesByUserId(int UserId);

        public Task UpdateNoteAsync(Note note);

    }
}
