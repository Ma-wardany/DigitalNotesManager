using DigitalNotesManager.Domain.Models;
using DigitalNotesManager.Services.ServiceImp;

namespace DigitalNotesManager
{
    internal class Program
    {
        //DigitalNotesManager
        static async Task Main(string[] args)
        {
            var userService = new UserService();

            //var user1 = await userService.LoginAsync("aya", "1234");
            //var user2 = await userService.LoginAsync("mahmoud", "1234");

            //Console.WriteLine($"{user1.Status} -- {user1.Message}");
            //Console.WriteLine($"{user2.Status} -- {user2.Message}");



            //User user = new User { UserName = "Mahmoud", PasswordHash = "1234" };
            //User user2 = new User { UserName = "aya", PasswordHash = "1234" };
            //var response = await userService.RegisterAsync(user);
            //var response2 = await userService.RegisterAsync(user2);

            //Console.WriteLine($"{response.Status} -- {response.Message}");
            //Console.WriteLine($"{response2.Status} -- {response2.Message}");


            //var categoryService = new CategoryService();
            //var category = new Category
            //{
            //    Name = "cat1",
            //    UserId = 1
            //};            
            //var category2 = new Category
            //{
            //    Name = "cat2",
            //    UserId = 1
            //};            
            //var category3 = new Category
            //{
            //    Name = "cat3",
            //    UserId = 2
            //};            
            //var category4 = new Category
            //{
            //    Name = "cat4",
            //    UserId = 2
            //};
            //var category5 = new Category
            //{
            //    Name = "cat5",
            //    UserId = 2
            //};

            //var response1 = await categoryService.AddCategory(category);
            //var response = await categoryService.AddCategory(category2);
            //var response2 = await categoryService.AddCategory(category3);
            //var response3 = await categoryService.AddCategory(category4);
            //var response4 = await categoryService.AddCategory(category5);

            //Console.WriteLine($"Result Message: {response1.Message}");

            //var categories = await categoryService.GetCategoriesByUserId(1);
            //var data = categories.Data;

            //foreach (var cat in data)
            //{
            //    Console.WriteLine($"Category ID: {cat.CategoryId}, Name: {cat.Name}");
            //}

            //var response = await categoryService.DeleteCategory(1, 1);
            //Console.WriteLine(response.Message);



            var noteService = new NotesServices();
            ////var notes = await noteService.GetNotesByCategoryId(6, 1);

            //var note = await noteService.GetNoteById(10, 2);

            //if(note.Status)
            //    Console.WriteLine($"Id: {note.Data.Id} -- content: {note.Data.Content}");
            //else
            //    Console.WriteLine("note not found");


            //var note2 = new Note
            //{
            //    Title = "Note 1",
            //    Content = "This is the content of note 1",
            //    CreationDate = DateTime.Now,
            //    ReminderDate = DateTime.Now.AddDays(1),
            //    UserId = 1,
            //    CategoryId = 6
            //};

            //var response = await noteService.AddNote(note2);
            //if (response.Status)
            //{
            //    Console.WriteLine($"Note added successfully: {response.Data.Title}");
            //}
            //else
            //{
            //    Console.WriteLine($"Failed to add note: {response.Message}");
            //}

            //var delete = await noteService.DeleteNote(18, 1);

            //Console.WriteLine($"Message: {delete.Message}");

            //var noteToUpdate = new Note
            //{
            //    Id = 1,
            //    Title = "Updated Note",
            //    Content = "This is the updated content of the note",
            //    CreationDate = DateTime.Now,
            //    ReminderDate = DateTime.Now.AddDays(2),
            //    UserId = 1,
            //    CategoryId = 6
            //};

            //var updateResponse = await noteService.UpdateNote(noteToUpdate);

            //if(!updateResponse.Status)
            //{
            //    Console.WriteLine(updateResponse.Message);
            //    Console.WriteLine(updateResponse.Status);
            //    return;
            //}
            //Console.WriteLine($"new title: {updateResponse.Data.Title}");


            //var testDate = new DateTime(2025, 6, 1);
            //var notes = await noteService.GetAllNotes(1);

            //var filteredNotes = await noteService.FilterNotesByDate(testDate, notes.Data);

            //if (filteredNotes.Status)
            //{
            //    Console.WriteLine($"Filtered Notes on {testDate.ToShortDateString()}:");
            //    foreach (var note in filteredNotes.Data)
            //    {
            //        Console.WriteLine($"Id: {note.Id}, Title: {note.Title}, Content: {note.Content}");
            //    }
            //}
            //else
            //{
            //    Console.WriteLine($"No notes found for the date {testDate.ToShortDateString()}");
            //}


            var searchText = "noTe";

            var searchResponse = await noteService.SearchNotes(2, searchText);

            if (searchResponse.Status)
            {
                Console.WriteLine($"Notes containing '{searchText}':");
                foreach (var note in searchResponse.Data)
                {
                    Console.WriteLine($"Id: {note.Id}, Title: {note.Title}, Content: {note.Content}");
                }
            }
            else
            {
                Console.WriteLine($"No notes found containing '{searchText}'");
            }
        }
    }
}
