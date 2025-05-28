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
    public class UserService : IUserService
    {
        private readonly DataContext _context;
        public UserService( DataContext context)
        {
            _context = context;
            
        }
        public async Task<User?> getBbyIdAsync(int id)
        {
           return await _context.Users.FindAsync(id);
        }

        public async Task<User> loginAsync(string username, string password)
        {
            return await _context.Users
                 .FirstOrDefaultAsync(u => u.userName == username && u.Password == password);
        }

        public async Task registerAsync(User user)
        {
           _context.Users.Add(user);
            await _context.SaveChangesAsync();
        }
    }
}
