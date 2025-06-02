using DigitalNotesManager.Domain.Models;
using DigitalNotesManager.Infrastructure.Data;
using DigitalNotesManager.Infrastructure.Repos.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DigitalNotesManager.Infrastructure.Repos.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _context;

        public UserRepository()
        {
            _context = new DataContext();

        }

        //public IQueryable<User> GetUsers()
        //{
        //    return _context.Users;
        //}

        public async Task<User?> getBbyIdAsync(int id)
        {
            return await _context.Users.FindAsync(id);
        }

        //public async Task<User> loginAsync(string username, string password)
        //{

        //    return await _context.Users
        //         .FirstOrDefaultAsync(u => u.UserName == username && u.PasswordHash == password);
        //}

        public async Task registerAsync(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
        }

        public async Task<User> FindUserByUsername(string username)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.UserName == username);
        }
    }
}
