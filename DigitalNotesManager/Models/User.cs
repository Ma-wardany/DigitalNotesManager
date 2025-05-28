using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalNotesManager.Models
{
    public class User
    {
        public int userId {  get; set; }
        public string userName {  get; set; }
        public string Password {  get; set; }

        public ICollection<Note> Notes { get; set; } //1:m

    }
}
