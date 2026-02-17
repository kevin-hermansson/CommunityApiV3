using CommunityApiV3.Models;
using CommunityApiV3.Repositories.Interfaces;
using CommunityApiV3.Services.Interfaces;

namespace CommunityApiV3.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        
        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<List<Category>> GetAllAsync()
        {
            return await _categoryRepository.GetAllAsync();
        }

        public async Task<Category?> GetByIdAsync(int id)
        {
            return await _categoryRepository.GetByIdAsync(id);
        }
        public async Task CreateAsync(Category category)
        {
            await _categoryRepository.AddAsync(category);
        }
    }
}
