using DigitalNotesManager.Domain.Models;
using System;

namespace DigitalNotesManager.Infrastructure.Repos.Interfaces
{
    public interface IUserRepository
    {
        //public IQueryable<User> GetUsers();
        Task registerAsync(User user);
        Task<User> getBbyIdAsync(int id);

        Task<User> FindUserByUsername(string username);
    }
}

//Task<User> loginAsync(string username, string password);