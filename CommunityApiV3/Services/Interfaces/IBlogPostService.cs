using CommunityApiV3.Models;

namespace CommunityApiV3.Services.Interfaces
{
    public interface IBlogPostService
    {
        Task<List<BlogPost>> GetAllAsync();
        Task<BlogPost?> GetByIdAsync(int id);
        Task<List<BlogPost>> SearchByTitleAsync(string title);
        Task<List<BlogPost>> GetByCategoryAsync(int categoryId);
        Task<bool> CreateAsync (BlogPost post);
        Task<string> UpdateAsync (int id, int userId, BlogPost updatedPost);
        Task<string> DeleteAsync(int id, int userId);

    }
}
