using CommunityApiV3.Data;
using CommunityApiV3.Data.Repositories.Interfaces;
using CommunityApiV3.Models;
using Microsoft.EntityFrameworkCore;

namespace CommunityApiV3.Data.Repositories
{
    public class CommentRepository : ICommentRepository
    {
        private readonly CommunityDbContext _db;

        public CommentRepository(CommunityDbContext db)
        {
            _db = db;
        }
        public async Task AddAsync(Comment comment)
        {
            await _db.Comments.AddAsync(comment);
            await _db.SaveChangesAsync();
        }

        public async Task<List<Comment>> GetByBlogPostIdAsync(int blogPostId)
        {
            return await _db.Comments.Include(c => c.User).Where(c => c.BlogPostId == blogPostId).ToListAsync();
        }
    }
}
