using DigitalNotesManager.Domain.DTOs;
using DigitalNotesManager.Domain.Models;
using DigitalNotesManager.Helpers;
using DigitalNotesManager.Infrastructure.Repos.Interfaces;
using DigitalNotesManager.Infrastructure.Repos.Repository;
using DigitalNotesManager.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using static Azure.Core.HttpHeader;

namespace DigitalNotesManager.Services.ServiceImp
{
    public class NotesServices : INotesServices
    {
        private readonly INoteRepository _noteRepository;
        private readonly IUserRepository _userRepository;
        private readonly ICategoryRepository _categoryRepository;

        public NotesServices()
        {
            _userRepository = new UserRepository();
            _noteRepository = new NoteRepository();
            _categoryRepository = new CategoryRepository();
        }

        public async Task<Response<IQueryable<NoteDto>>> GetNotesByCategoryId(int categoryId, int userId)
        {
            var user = await _userRepository.getBbyIdAsync(userId);
            if (user == null)
                return Response<IQueryable<NoteDto>>.Failure("User not found");

            var category = await _categoryRepository.GetCategoryByIdAsync(categoryId);
            if (category == null)
                return Response<IQueryable<NoteDto>>.Failure("Category not found");

            try
            {
                var notes = _noteRepository.GetNoteByCategory(categoryId, userId);
                if (!notes.Any())
                    return Response<IQueryable<NoteDto>>.Failure("No matched notes");

                var noteDtos = GetMappedNotes(notes); // Use single mapping function for IQueryable

                return Response<IQueryable<NoteDto>>.Success(noteDtos, "Notes retrieved successfully");
            }
            catch (Exception ex)
            {
                return Response<IQueryable<NoteDto>>.Failure($"An error occurred while retrieving notes: {ex.Message}");
            }
        }

        public async Task<Response<IQueryable<NoteDto>>> GetAllNotes(int userId)
        {
            var user = await _userRepository.getBbyIdAsync(userId);
            if (user == null)
                return Response<IQueryable<NoteDto>>.Failure("User not found");

            try
            {
                var notes = _noteRepository.GetNotesByUserId(userId);
                if (!notes.Any())
                    return Response<IQueryable<NoteDto>>.Failure("No notes found for this user");

                var noteDtos = GetMappedNotes(notes); // Use single mapping function for IQueryable

                return Response<IQueryable<NoteDto>>.Success(noteDtos, "Notes retrieved successfully");
            }
            catch (Exception ex)
            {
                return Response<IQueryable<NoteDto>>.Failure($"An error occurred while retrieving notes: {ex.Message}");
            }
        }

        public async Task<Response<NoteDto>> GetNoteById(int noteId, int userId)
        {
            var user = await _userRepository.getBbyIdAsync(userId);
            if (user == null)
                return Response<NoteDto>.Failure("User not found");

            try
            {
                var note = await _noteRepository.GetNoteById(noteId);
                if (note == null)
                    return Response<NoteDto>.Failure("Note not found");

                if (note.UserId != userId)
                    return Response<NoteDto>.Failure("No notes matched with this user");

                var noteDto = GetMappedNotes(note); // Use single mapping function for single Note

                return Response<NoteDto>.Success(noteDto, "Note retrieved successfully");
            }
            catch (Exception ex)
            {
                return Response<NoteDto>.Failure($"An error occurred while retrieving the note: {ex.Message}");
            }
        }

        public async Task<Response<NoteDto>> AddNote(Note note)
        {
            var user = await _userRepository.getBbyIdAsync(note.UserId);
            if (user == null)
                return Response<NoteDto>.Failure("User not found");

            var category = await _categoryRepository.GetCategoryByIdAsync(note.CategoryId);
            if (category == null)
                return Response<NoteDto>.Failure("Category not found");

            if (category.UserId != note.UserId)
                return Response<NoteDto>.Failure("Category does not belong to this user");


            var existingNote = _noteRepository.GetNotesByUserId(note.UserId)
                .FirstOrDefault(n => n.Title.ToLower() == note.Title.ToLower() && n.CategoryId == note.CategoryId);

            if (existingNote != null)
                return Response<NoteDto>.Failure("Note with this title already exists in the category");

            try
            {
                await _noteRepository.AddNoteAsync(note);
                return Response<NoteDto>.Success(GetMappedNotes(note), "Note added successfully");
            }
            catch (Exception ex)
            {
                return Response<NoteDto>.Failure($"An error occurred while adding the note: {ex.Message}");
            }
        }

        public async Task<Response<NoteDto>> UpdateNote(Note note)
        {

            var existingNote = await _noteRepository.GetNoteById(note.Id);

            if (existingNote == null || existingNote.UserId != note.UserId || existingNote.CategoryId != note.CategoryId)
                return Response<NoteDto>.Failure("Note not found");


            try
            {
                existingNote.Title        = note.Title;
                existingNote.Content      = note.Content;
                existingNote.CategoryId   = note.CategoryId;
                existingNote.ReminderDate = note.ReminderDate;


                await _noteRepository.UpdateNoteAsync(existingNote);
                var updatedNoteDto = GetMappedNotes(existingNote);


                return Response<NoteDto>.Success(updatedNoteDto, "Note updated successfully");
            }
            catch (Exception ex)
            {
                return Response<NoteDto>.Failure($"An error occurred while updating the note: {ex.Message}");
            }
        }

        public async Task<Response<string>> DeleteNote(int noteId, int userId)
        {
            var user = await _userRepository.getBbyIdAsync(userId);
            if (user == null)
                return Response<string>.Failure("User not found");

            var note = await _noteRepository.GetNoteById(noteId);
            if (note == null)
                return Response<string>.Failure("Note not found");

            if (note.UserId != userId)
                return Response<string>.Failure("Note does not belong to this user");


            try
            {
                await _noteRepository.DeleteNoteAsync(note);
                return Response<string>.Success("Note deleted successfully");
            }
            catch (Exception ex)
            {
                return Response<string>.Failure($"An error occurred while deleting the note: {ex.Message}");
            }

        }

        public async Task<Response<IQueryable<NoteDto>>> SearchNotes(int userId, string searchText)
        {
            searchText = searchText.ToLower();

            var notes = _noteRepository.GetNotesByUserId(userId)
                .Where(note => note.Title.ToLower().Contains(searchText) ? true
                               : note.Content.ToLower().Contains(searchText));

            if (!notes.Any())
                return Response<IQueryable<NoteDto>>.Failure("No notes found matching the search criteria");

            var noteDtos = GetMappedNotes(notes);

            return Response<IQueryable<NoteDto>>.Success(noteDtos, "Notes retrieved successfully");

        }




        public async Task<Response<IQueryable<NoteDto>>> FilterNotesByDate(DateTime date, IQueryable<NoteDto> notes)
        {
            // Convert to client evaluation to avoid translation issues
            var filteredNotes = notes.AsEnumerable()
                .Where(note => note.CreationDate.Date == date.Date)
                .AsQueryable();

            if (!filteredNotes.Any())
                return Response<IQueryable<NoteDto>>.Failure("No notes found for the specified date");

            return Response<IQueryable<NoteDto>>.Success(filteredNotes, "Notes filtered by date successfully");
        }

        private static NoteDto GetMappedNotes(Note note)
        {
            return new NoteDto
            {
                Id = note.Id,
                Title = note.Title,
                Content = note.Content,
                CreationDate = note.CreationDate,
                ReminderDate = note.ReminderDate
            };
        }

        private IQueryable<NoteDto> GetMappedNotes(IQueryable<Note> notes)
        {
            return notes.Select(note => GetMappedNotes(note));
        }
    }
}