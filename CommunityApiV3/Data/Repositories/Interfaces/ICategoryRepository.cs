using CommunityApiV3.Models;

namespace CommunityApiV3.Data.Repositories.Interfaces
{
    public interface ICategoryRepository
    {
        Task<List<Category>> GetAllAsync();
        Task<Category?> GetByIdAsync(int id);
        Task<int> AddAsync(Category category);
        Task DeleteAsync(Category category);
    }

}
