using CommunityApiV3.DTOs.BlogPosts;

namespace CommunityApiV3.Services.Interfaces
{
    public interface IBlogPostService
    {
        Task<List<BlogPostResponseDto>> GetAllAsync();
        Task<BlogPostResponseDto?> GetByIdAsync(int id);
        Task<List<BlogPostResponseDto>> GetByTitleAsync(string title);
        Task<List<BlogPostResponseDto>> GetByCategoryAsync(int categoryId);

        Task<int?> CreateAsync(CreateBlogPostDto dto);
        Task<bool> UpdateAsync(int id, UpdateBlogPostDto dto);
        Task<bool> DeleteAsync(int id, int userId);
    }
}
