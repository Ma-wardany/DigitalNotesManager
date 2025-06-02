using DigitalNotesManager.Domain.DTOs;
using DigitalNotesManager.Domain.Models;
using DigitalNotesManager.Helpers;

namespace DigitalNotesManager.Services.Interfaces
{
    public interface IUserService
    {
        public Task<Response<UserDto>> RegisterAsync(User user);
        public Task<Response<UserDto>> LoginAsync(string username, string password);
    }
}
