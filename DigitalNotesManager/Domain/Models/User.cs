namespace DigitalNotesManager.Domain.Models
{
    public class User
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string PasswordHash { get; set; } // Renamed to indicate hashed password
        public DateTime CreatedAt { get; set; }

        public virtual ICollection<Category> Categories { get; set; } = new List<Category>();
        public virtual ICollection<Note> Notes { get; set; } = new List<Note>();
    }
}