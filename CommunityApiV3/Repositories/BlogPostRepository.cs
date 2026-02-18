using CommunityApiV3.Data;
using CommunityApiV3.Models;
using CommunityApiV3.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CommunityApiV3.Repositories
{
    public class BlogPostRepository : IBlogPostRepository
    {
        private readonly CommunityDbContext _db;

        public BlogPostRepository(CommunityDbContext db)
        {
            _db = db;
        }

        public async Task<List<BlogPost>> GetAllAsync()
        {
            return await _db.Blogposts
                .Include(p => p.User)
                .Include(p => p.Category)
                .ToListAsync();
        }

        public async Task<BlogPost?> GetByIdAsync(int id)
        {
            return await _db.Blogposts
                .Include(p => p.User)
                .Include(p => p.Category)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<List<BlogPost>> GetByTitleAsync(string title)
        {
            return await _db.Blogposts
                .Include(p => p.User)
                .Include(p => p.Category)
                .Where(p => p.Title.Contains(title))
                .ToListAsync();
        }

        public async Task<List<BlogPost>> GetByCategoryAsync(int categoryId)
        {
            return await _db.Blogposts
                .Include(p => p.User)
                .Include(p => p.Category)
                .Where(p => p.CategoryId == categoryId)
                .ToListAsync();
        }

        public async Task AddAsync(BlogPost post)
        {
            await _db.Blogposts.AddAsync(post);
            await _db.SaveChangesAsync();
        }

        public async Task UpdateAsync(BlogPost post)
        {
            _db.Blogposts.Update(post);
            await _db.SaveChangesAsync();
        }

        public async Task DeleteAsync(BlogPost post)
        {
            _db.Blogposts.Remove(post);
            await _db.SaveChangesAsync();
        }
    }
}
