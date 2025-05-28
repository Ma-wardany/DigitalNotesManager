using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalNotesManager.Models
{
    public class Note
    {
        public int noteId {  get; set; }
        public string Title {  get; set; }
        public string Content {  get; set; }
        public string Category {  get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime ReminderDate { get; set; }

        public int userId {  get; set; }
        public User User { get; set; }
    }
}
