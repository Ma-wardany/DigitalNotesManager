using DigitalNotesManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DigitalNotesManager.Interfaces
{
    public interface IUserService
    {
        Task<User> loginAsync(string username, string password);
        Task registerAsync(User user);
        Task<User?> getBbyIdAsync(int id);
    }
}
