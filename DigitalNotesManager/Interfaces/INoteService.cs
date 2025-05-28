using DigitalNotesManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalNotesManager.Interfaces
{
    public interface INoteService
    {
        Task<List<Note>> GetAllNotesAsync(int id);
        Task<Note> GetNoteById(int id);
        Task AddNoteAsync(Note note);
        Task UpdateNoteAsync(Note note);
        Task DeleteNoteAsync(int id);

    }
}
