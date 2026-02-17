using CommunityApiV3.DTOs.Users;
using CommunityApiV3.Models;

namespace CommunityApiV3.Services.Interfaces
{
    public interface IUserService
    {
        Task<List<UserResponseDto>> GetAllAsync();

        Task<User?> GetByIdAsync(int id);
        Task<int?> LoginAsync(string username, string password);
        Task<int> CreateAsync(CreateUserDto dto);

        Task<bool> UpdateAsync(int id, User updatedUser);
        Task<bool> DeleteAsync(int id);


    }
}
