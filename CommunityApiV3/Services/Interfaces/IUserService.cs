using CommunityApiV3.DTOs.Users;

namespace CommunityApiV3.Services.Interfaces
{
    public interface IUserService
    {
        Task<List<UserResponseDto>> GetAllAsync();
        Task<UserResponseDto?> GetByIdAsync(int id);
        Task<int> CreateAsync(CreateUserDto dto);
        Task<int?> LoginAsync(LoginDto dto);
        Task<bool> UpdateAsync(int id, UpdateUserDto dto);
        Task<bool> DeleteAsync(int id);
    }
}
