using CommunityApiV3.Models;

namespace CommunityApiV3.Repositories.Interfaces
{
    public interface IBlogPostRepository
    {
        Task<List<BlogPost>> GetAllAsync();
        Task<BlogPost?> GetByIdAsync(int id);
        Task<List<BlogPost>> GetByTitleAsync(string title);
        Task<List<BlogPost>> GetByCategoryAsync(int categoryId);
        Task AddAsync(BlogPost post);
        Task UpdateAsync(BlogPost post);
        Task DeleteAsync(BlogPost post);
    }
}
