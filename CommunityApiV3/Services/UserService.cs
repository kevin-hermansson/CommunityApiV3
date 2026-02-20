using CommunityApiV3.Data.Repositories.Interfaces;
using CommunityApiV3.DTOs.Users;
using CommunityApiV3.Models;
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

            return users.Select(u => new UserResponseDto
            {
                Id = u.Id,
                Username = u.Username,
                Email = u.Email
            }).ToList();
        }


        public async Task<UserResponseDto?> GetByIdAsync(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);

            if (user == null)
                return null;

            return new UserResponseDto
            {
                Id = user.Id,
                Username = user.Username,
                Email = user.Email
            };
        }

        public async Task<int> CreateAsync(CreateUserDto dto)
        {
            var existingUser = await _userRepository.GetByUsernameAsync(dto.Username);

            if (existingUser != null)
                return 0;

            var user = new User
            {
                Username = dto.Username,
                Password = dto.Password,
                Email = dto.Email
            };

            await _userRepository.AddAsync(user);

            return user.Id;
        }

        public async Task<int?> LoginAsync(LoginDto dto)
        {
            var user = await _userRepository.GetByUsernameAsync(dto.Username);

            if (user == null)
                return null;

            if (user.Password != dto.Password)
                return null;

            return user.Id;
        }

        public async Task<bool> UpdateAsync(int id, UpdateUserDto dto)
        {
            var user = await _userRepository.GetByIdAsync(id);

            if (user == null)
                return false;

            user.Username = dto.Username;
            user.Password = dto.Password;
            user.Email = dto.Email;

            await _userRepository.UpdateAsync(user);

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
