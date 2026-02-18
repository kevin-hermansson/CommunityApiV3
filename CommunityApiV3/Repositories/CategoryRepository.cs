using CommunityApiV3.Data;
using CommunityApiV3.Models;
using CommunityApiV3.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CommunityApiV3.Repositories
{

    public class CategoryRepository : ICategoryRepository
    {
        private readonly CommunityDbContext _db;

        public CategoryRepository (CommunityDbContext db)
        {
            _db = db;
        }

        public async Task<List<Category>> GetAllAsync()
        {
            return await _db.Categories.ToListAsync();
        }
        public async Task<int> AddAsync(Category category)
        {
            await _db.Categories.AddAsync(category);
            await _db.SaveChangesAsync();

            return category.Id;
        }



        public async Task<Category?> GetByIdAsync(int id)
        {
            return await _db.Categories.FindAsync(id);
        }

        public async Task DeleteAsync(Category category)
        {
            _db.Categories.Remove(category);
            await _db.SaveChangesAsync();
        }
    }
}
