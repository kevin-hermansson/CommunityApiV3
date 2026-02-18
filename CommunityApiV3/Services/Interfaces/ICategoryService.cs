using CommunityApiV3.DTOs.Categories;

namespace CommunityApiV3.Services.Interfaces
{
    public interface ICategoryService
    {
        Task<List<CategoryResponseDto>> GetAllAsync();

        Task<CategoryResponseDto?> GetByIdAsync(int id);
        Task<int> CreateAsync(CreateCategoryDto dto);
        Task<bool> DeleteAsync(int id);
    }
}
