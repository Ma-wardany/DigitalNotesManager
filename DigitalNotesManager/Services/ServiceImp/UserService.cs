using DigitalNotesManager.Domain.DTOs;
using DigitalNotesManager.Domain.Models;
using DigitalNotesManager.Helpers;
using DigitalNotesManager.Infrastructure.Repos.Interfaces;
using DigitalNotesManager.Infrastructure.Repos.Repository;
using DigitalNotesManager.Services.Interfaces;


namespace DigitalNotesManager.Services.ServiceImp
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        public UserService()
        {
            _userRepository = new UserRepository();
        }

        public async Task<Response<UserDto>> RegisterAsync(User user)
        {
            var existUser = await _userRepository.FindUserByUsername(user.UserName);

            if (existUser != null)
            {
                return Response<UserDto>.Failure("User already exists with this username");
            }

            try
            {
                string PasswordHash = BCrypt.Net.BCrypt.HashPassword(user.PasswordHash);

                user.PasswordHash = PasswordHash;
                user.CreatedAt = DateTime.UtcNow;

                await _userRepository.registerAsync(user);
                var userDto = new UserDto
                {
                    Id = user.Id,
                    UserName = user.UserName,
                    CreatedAt = user.CreatedAt
                };
                return Response<UserDto>.Success(userDto, "User registered successfully");
            }
            catch (Exception ex)
            {
                return Response<UserDto>.Failure($"An error occurred while registering the user: {ex.Message}");
            }
        }

        public async Task<Response<UserDto>> LoginAsync(string username, string password)
        {
            var existUser = await _userRepository.FindUserByUsername(username);
            var userDto = new UserDto
            {
                Id = existUser.Id,
                UserName = existUser.UserName,
                CreatedAt = existUser.CreatedAt
            };

            if (existUser == null)
                return Response<UserDto>.Failure("User is not found");


            if (!BCrypt.Net.BCrypt.Verify(password, existUser.PasswordHash))
            {
                return Response<UserDto>.Failure("Password is wrong");
            }

            return Response<UserDto>.Success(userDto, "User logged in successfully");

        }
    }
}
