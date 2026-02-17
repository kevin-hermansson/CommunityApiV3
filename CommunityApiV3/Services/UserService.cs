using CommunityApiV3.DTOs.Users;
using CommunityApiV3.Models;
using CommunityApiV3.Repositories.Interfaces;
using CommunityApiV3.Services.Interfaces;

namespace CommunityApiV3.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<List<UserResponseDto>> GetAllAsync()
        {
            var users = await _userRepository.GetAllAsync();

            return users.Select(u => new UserResponseDto {Id = u.Id, Username = u.Username, Email = u.Email }).ToList();
        }

        public async Task<User?> GetByIdAsync(int id)
        {
            return await _userRepository.GetByIdAsync(id);
        }

        public async Task<int?> LoginAsync(string username, string password)
        {
            var user = await _userRepository.GetByUsernameAsync(username);

            if (user == null)
                return null;

            if (user.Password != password)
                return null;

            return user.Id;
        }

        public async Task<int> CreateAsync(CreateUserDto dto)
        {
            var user = new User
            {
                Username = dto.Username,
                Password = dto.Password,
                Email = dto.Email
            };

            return await _userRepository.AddAsync(user);
        }


        public async Task<bool> UpdateAsync(int id, User updatedUser)
        {
            var existingUser = await _userRepository.GetByIdAsync(id);

            if (existingUser == null)
                return false;

            existingUser.Username = updatedUser.Username;
            existingUser.Password = updatedUser.Password;
            existingUser.Email = updatedUser.Email;

            await _userRepository.UpdateAsync(existingUser);

            return true;
        }
        public async Task<bool> DeleteAsync(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);

            if (user == null)
                return false;

            await _userRepository.DeleteAsync(user);

            return true;
        }
    }
}
